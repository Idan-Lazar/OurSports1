
/* when document is ready */
$(function () {
    /* initiate the plugin */
    $("div.holder").jPages({
        containerID: "itemContainer",
        animation: "bounceInUp",
        perPage: 5,
        startPage: 1,
        startRange: 1,
        midRange: 5,
        endRange: 1
    });
});

function countChar(val) {
    var len = val.value.length;
      var max = 200;
    if (len >= max) {
        val.value = val.value.substring(0, 500);
    } else {
        $('#charNum').text(max - len);
    }
};

/using jqery to cahnge the page nav style
$(".holder").has("a").ready(function () {
    $(".holder").wrapInner("<ul class='pagination'></ul>");
    $(".holder ul").css("width", "fit-content");
    $(".holder ul").addClass("m-auto p-2 text-md text-success ");
    $(".holder").addClass("Page navigation example m-0 ");
    $(".holder a").wrap("<li class='page- item'></li>");
    $(".holder a").addClass("page-link");
    
});


$(".Searchline input").click(function () {
    var id = this.id;
    var check = $(".Searchline input#"+id+":checked").length;
    if (check) {
        $(".Searchline select#" + id+"Select").removeAttr("disabled");
    }
    else {
        $(".Searchline select#" + id + "Select").prop("disabled", "true");
    }
 
   
});