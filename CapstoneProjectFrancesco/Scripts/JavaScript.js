document.addEventListener('DOMContentLoaded', function () {
    // Quando il DOM è pronto, rendi la content-container visibile e spostala verso l'alto
    const contentContainer = document.querySelector('.content-container');
    contentContainer.style.opacity = 1;
    contentContainer.style.transform = 'translateY(0)';
});

// Aggiungi un effetto di caricamento durante lo scroll
window.addEventListener('scroll', function () {
    const contentContainer = document.querySelector('.content-container');
    const contentContainerBottom = contentContainer.getBoundingClientRect().bottom;
    const windowHeight = window.innerHeight;

    // Se la content-container è visibile e si è scrollati abbastanza, aggiungi i tuoi effetti di caricamento
    if (contentContainerBottom < windowHeight && contentContainer.style.opacity === '1') {
        // Aggiungi qui i tuoi effetti di caricamento
        console.log('Carica più contenuti durante lo scroll!');
    }
});
