FROM node:18 AS build
WORKDIR /app
RUN apt-get update && apt-get install -y git
ARG REPO_URL=https://github.com/lordstimpa/StevenSoftware.git
RUN git clone $REPO_URL . && cd stevensoftware.client

COPY package.json package-lock.json ./
RUN npm install
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]