 
 
 
 
 /*==================== SCROLL REVEAL ANIMATION ====================*/
const sr = ScrollReveal({
    origin: 'top',
   distance: '30px',
    duration: 2000,
    reset: true
});

sr.reveal(`.home__data, .container, #graph, #graph2, #graph3, .home__img,
           .about__data, .about__img,
           .services__content, .menu__content,
            .app__data, .app__img,
            .contact__data, .contact__button,
             .footer__content`, {
     interval: 200
 })
