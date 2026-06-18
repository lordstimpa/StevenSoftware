export default {
    mounted(el, binding) {
      const type = typeof binding.value === 'string' ? binding.value : 'fade-up'

      el.setAttribute('data-animate', type)

      const observer = new IntersectionObserver(
        ([entry]) => {
          if (entry.isIntersecting) {
            el.classList.add('in-view')
            observer.unobserve(el)
          }
        },
        {
          threshold: 0.1,
          rootMargin: '0px 0px -120px 0px'
        }
      )

      observer.observe(el)
    }
};
