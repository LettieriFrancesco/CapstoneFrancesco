const TornaSuHome = document.getElementById('Up')
const box = document.getElementById('boxTornaSu')

document.addEventListener('scroll', function () {
    if (window.scrollY > 1200) {
        TornaSuHome.classList.add('show');
        box.classList.add('show');
    } else {
        TornaSuHome.classList.remove('show');
        box.classList.remove('show');
    }
});
