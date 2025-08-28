#!/usr/bin/env node

import fs from "fs";
import https from "https";
import fetch from "node-fetch";
import "dotenv/config";

const API_URL = `${process.env.VITE_API_URL}/blog/getblogposts?pageNumber=1`;
const BASE_URL = process.env.VITE_URL.replace(
  "localhost:7271",
  "stevensoftware.se"
);

const httpsAgent = new https.Agent({
  rejectUnauthorized: false,
});

async function generateSitemap() {
  try {
    const response = await fetch(API_URL, {
      headers: {
        Authorization: `Bearer ${process.env.JWT_TOKEN || ""}`,
      },
      agent: httpsAgent,
    });

    if (!response.ok) {
      throw new Error(`API request failed: ${response.statusText}`);
    }

    const data = await response.json();
    const blogPosts = data.data?.blogPosts || [];

    const urls = [
      `${BASE_URL}/`,
      `${BASE_URL}/services`,
      `${BASE_URL}/case-studies`,
      `${BASE_URL}/blog`,
      ...blogPosts.map((post) => `${BASE_URL}/blog/${post.id}`),
    ];

    const sitemap = `<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
${urls
        .map(
          (url) => `
  <url>
    <loc>${url}</loc>
    <changefreq>weekly</changefreq>
  </url>`
        )
        .join("")}
</urlset>
`;

    if (!fs.existsSync("public")) {
      fs.mkdirSync("public");
    }

    fs.writeFileSync("public/sitemap.xml", sitemap, "utf-8");
    console.log("✅ Sitemap generated: public/sitemap.xml");
  } catch (err) {
    console.error("❌ Error generating sitemap:", err.message);
    process.exit(1);
  }
}

generateSitemap();
