const hamburger = document.querySelector('.hamburger');
const hamburger_icon = hamburger.querySelector('span');
const mobile_menu = document.querySelector('.mobile-menu');

hamburger.addEventListener('click', () => {
	hamburger_icon.innerText = hamburger_icon.innerText === 'menu'
		? 'close'
		: 'menu';

	mobile_menu.classList.toggle('is-open');
})


function yesnoCheck() {
	if (document.getElementById('secondRadio').checked) {
		document.getElementById('profEmail').style.display = "block";
	}
	else {
		document.getElementById('profEmail').style.display = "none";
    }
}


//---------------------------------------------------------------------------
const selectBtn = document.querySelector(".select-btn"),
    items = document.querySelectorAll(".item");

selectBtn.addEventListener("click", () => {
    selectBtn.classList.toggle("open");
});

items.forEach(item => {
    item.addEventListener("click", () => {
        item.classList.toggle("checked");

        let checked = document.querySelectorAll(".checked"),
            btnText = document.querySelector(".btn-text");

        if (checked && checked.length > 0) {
            btnText.innerText = `${checked.length} Selected`;
        } else {
            btnText.innerText = "Add Skill";
        }
    });
})

//---------------------------------------------------------

function addAllSkills() {
    let checked = document.querySelectorAll(".item.checked .item-text");
    var returnList = [];

    for (let i = 0; i < check.length; i++) {
        returnList[i] = checked[i].innerText;
    }

    document.write(returnList[0]);

}

//------------------------------------------------------------

function myFunction(s) {
    alert(this.s);
}



//---------------------------------------------------------


const fruits = ["Banana", "Orange", "Apple", "Mango"];
let fLen = fruits.length;

let text = "<ul>";
for (let i = 0; i < fLen; i++) {
  text += "<li>" + fruits[i] + "</li>";
}
text += "</ul>";

