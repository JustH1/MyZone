let showUlOrderProducts = document.querySelectorAll('showUlOrderProducts');
let ulOrderProducts = document.getElementById('.ulOrderProducts');

showUlOrderProducts.addEventListener('click', () => {
    if (ulOrderProducts.style.display === 'none') {
        ulOrderProducts.style.display = '';
    }
    else {
        ulOrderProducts.style.display = 'none';
    }
});