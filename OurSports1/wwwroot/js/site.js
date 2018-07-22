
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
$(function (){
    var live;
   
    $.ajax({
        url: "https://apifootball.com/api/?action=get_events&from=2017-04-01&to=2017-04-24&league_id=150&APIkey=df4ea49a08ed63529e46516ab69c3012e32ac721aafa8186a1d8c05fd61a5df6",
        dataType: 'json', 
        type: 'get', 
        cache: false,
        success: function (data) {
            $(data).each(function (index, value) {
                if (index < 5) {

                    $(".containerjson").append("<tr class='score " + index + "'></tr>");
                    $(".containerjson tr.score." + index).wrapInner("<table class='table'></table>").addClass("box");
                    $(".containerjson tr.score." + index + " table").append("<tr class='score '><td class='country_name'>" + value.country_name + "</td></tr>");
                    $(".containerjson tr.score." + index + " table").append("<tr class='score '><td class='league_name'>" + value.league_name + "</td></tr>");
                    $(".containerjson tr.score." + index + " table").append("<tr class='score '><td class='match_date'>" + value.match_date + "</td></tr>");
                    $(".containerjson tr.score." + index + " table").append("<tr class='score '><td class='match_hometeam_name'>" + value.match_hometeam_name +
                        "</td>" + "<td class='match_hometeam_score'>" + value.match_hometeam_score + "</td>" + "</td>" + "<td class='match_awayteam_name'>" + value.match_awayteam_name + "</td>" + "<td class='match_awayteam_score'>" + value.match_awayteam_score + "</td>" + "</tr>");
                    $("td.country_name").addClass("text-danger font-weight-bold");

                } });
        }
    });
});
