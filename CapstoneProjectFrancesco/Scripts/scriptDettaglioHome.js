// scriptDettaglioHome.js
console.log('Script caricato');

document.addEventListener('DOMContentLoaded', function () {
    animateElement('.content-container', 0);
    setTimeout(() => animateElement('.product-image', 500), 500);
    setTimeout(() => animateElement('.product-description', 1000), 1000);
});

function animateElement(selector, delay) {
    const element = document.querySelector(selector);
    element.style.opacity = 1;
    element.style.transform = 'translateY(0)';
    element.style.transition = `opacity 1s ease, transform 1s ease ${delay}ms`;
}

// Aggiungi un effetto di caricamento durante lo scroll (se necessario)
window.addEventListener('scroll', function () {
    // Implementa qui l'effetto di caricamento durante lo scroll, se necessario
    // Puoi seguire lo stesso approccio usato nella pagina home
});
