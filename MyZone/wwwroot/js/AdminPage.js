//Left panel buttons
let statisticsPanelButton = document.getElementById('statisticsPanelButton');
let searchingChangingDataButton = document.getElementById('searchingChangingDataButton');
//Panels inside the container
let statisticsPanel = document.getElementById('statisticsPanel'); 
let searchingChangingData = document.getElementById('searchingChangingData');

let choosingEntity = document.getElementById('choosingEntity');

let formChangeReviews = document.getElementById('formChangeReviews');
let formChangeOrder = document.getElementById('formChangeOrder');
let formChangeUser = document.getElementById('formChangeUser');


//Сhanging the color of the buttons
//in the menu and displaying the desired panel.
statisticsPanelButton.addEventListener('click', ()=> {
    searchingChangingData.style.display = 'none';
    searchingChangingDataButton.style.color = 'rgb(100,102,123)';
    statisticsPanel.style.display = '';
    statisticsPanelButton.style.color = 'white';
});
searchingChangingDataButton.addEventListener('click',() => {
    searchingChangingData.style.display = '';
    searchingChangingDataButton.style.color = 'white';
    statisticsPanel.style.display = 'none';
    statisticsPanelButton.style.color = 'rgb(100,102,123)';
});
statisticsPanel.click();

