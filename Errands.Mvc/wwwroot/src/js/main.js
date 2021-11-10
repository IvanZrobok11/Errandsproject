///header__fixed
window.onscroll = function(){
    let header = document.querySelector(".header");

    if(window.pageYOffset > 300){
        header.classList.add("header__fixed");
    }
    else{
        header.classList.remove("header__fixed");
    }
}
///header__fixed
const burger = document.querySelector('.menu__icon');
const menuBody = document.querySelector('.menu__body');
if(burger) {
    burger.addEventListener('click', onBurgerClick);
    function onBurgerClick() {
        document.body.classList.toggle('_lock');
        burger.classList.toggle('_active');
        menuBody.classList.toggle('_active');
    }
}

//////
/*const menuSubLists = document.querySelectorAll('.menu__sub-list');
const menuArrow = document.querySelector('.arrow-down');
if(menuSubList) {
    //arrowDown.addEventListener('click', onBurgerClick);

    if (menuSubLists.length > 0) {
        for (let index = 0; index < menusublists.length; index++) {
            const menuSubList = menuSubLists[index];
            menuArrow.addEventListener("click", function(e){
                menuArrow.classList.toggle('_active');
                menuSubList.classList.toggle('_active');
            });
        }
    }
}*/
let menuArrows = document.querySelectorAll('.menu__click');
    if (menuArrows.length > 0) {
        for (let index = 0; index < menuArrows.length; index++) {
            const menuArrow = menuArrows[index];
            menuArrow.addEventListener("click", function(e){
                menuArrow.parentElement.classList.toggle('_active');
            });
        }
    }



