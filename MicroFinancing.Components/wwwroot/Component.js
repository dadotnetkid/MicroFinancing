function ShowToast(elementId) {
   /* let myToastEl = document.getElementById(elementId);
    let myToast = bootstrap.Toast.getOrCreateInstance(myToastEl);
    myToast.show();*/
    $('#' + elementId).toast('show')

} function ShowToastByClass() {
    let myToastEl = document.querySelector('.toast');
    let myToast = bootstrap.Toast.getOrCreateInstance(myToastEl);
    myToast.show();
}