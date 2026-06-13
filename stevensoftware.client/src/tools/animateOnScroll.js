export default {
  mounted(el, binding) {
    el.setAttribute('data-animate', binding.value || 'fade-up');

    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          el.classList.add('in-view');
          observer.unobserve(el);
        }
      },
      { threshold: 0.15 }
    );

    observer.observe(el);
  }
};
