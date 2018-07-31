
/* when document is ready */
$(function () {
    /* initiate the plugin */
    /* jPAGES - plugin setting*/
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

/*max words on comment*/
function countChar(val) {
    var len = val.value.length;
    var max = 200;
    if (len >= max) {
        val.value = val.value.substring(0, 200);
    } else {
        $('#charNum').text(max - len);
    }
};

/*adding style to the plugin - Jpages */
$(".holder").has("a").ready(function () {
    $(".holder").wrapInner("<ul class='pagination'></ul>");
    $(".holder ul").css("width", "fit-content");
    $(".holder ul").addClass("m-auto p-2 text-md text-success ");
    $(".holder").addClass("Page navigation example m-0 ");
    $(".holder a").wrap("<li class='page-item'></li>");
    $(".holder a").addClass("page-link");

});

/*Filter form inline , Enable the select by the check*/
$(".Searchline input").click(function () {
    var id = this.id; 
    var check = $(".Searchline input#" + id + ".form-check-input:checked").length;
    if (check) {
        $(".Searchline select#" + id + "Select").removeAttr("disabled");
        $(".Searchline input#" + id + "Input").removeAttr("disabled");
    }
    else {
        $(".Searchline select#" + id + "Select").prop("disabled", "true");
        $(".Searchline input#" + id + "Input").prop("disabled", "true");
    }


});
/* Make list of the writers of the current Category - Join Qeury*/
$(function () {
    $('#btnAuthorsGet').one("click", function () {
        var idcat = document.getElementsByClassName("categoryid")[0].id;
        $(".AuthorsTable").html();
        $.ajax({
            type: 'GET',
            data: { Categoryid: idcat },
            url: '/Categories/CategoryAjax',
            success: function (result) {
                $(result).each(function (index, value) {
                    $(".AuthorsTable").append("<tr class='box " + index + "'></tr>");
                    $(".AuthorsTable tr.box." + index).append("<a class='btn p-2 btn-primary' href='/Authors/Details/?id=" + value.id + "'>" + value.authorName + "</a>");


                });
            }
        });

    });
});
/* Make list of the top 5 comments of the auhtor - Join Qeury*/
$(function () {
    $('#btnCommentGet').one("click", function () {
        var idaut = document.getElementsByClassName("authorid")[0].id;
        $(".CommentTable.Author").html();

        $.ajax({
            type: 'GET',
            data: { Authorid: idaut },
            url: '/Authors/AuthorsAjax',
            success: function (result) {
                $(".CommentTable.Author").append("<tr class='comments rounded shadow shadow-sm'><tr>");
                $(result).each(function (index, value) {
                   
                    $(".CommentTable.Author tr.comments ").append(" <table class='" + index + " comment'><tr class='1'><td> <span class= 'text-capitalize text-danger font-weight-bold text-lg-right' > Title:</span> <td><td> <span class= 'text-capitalize text-dark  text-lg-right' >" + value.title + "</span> <td><td> <span class= 'text-capitalize text-danger font-weight-bold text-lg-right' > Writer Name:</span> <td><td> <span class= 'text-capitalize text-dark  text-lg-right' >" + value.writerName + "</span> <td></tr><tr class='2'></tr><td> <span class= 'text-capitalize text-danger font-weight-bold text-lg-right' > Content:</span> <td><td colspan='3'> <span class= 'text-capitalize text-dark  text-lg-right' >" + value.content + "</span> <td></table>");
                
                });
                $(".CommentTable.Author").addClass("comments table");
            }
        });

    });

});

/* Make list of Categories in NavBar*/
$(function () {
 

        $.ajax({
            type: 'GET',
            url: '/Categories/ListNameCategories',
            success: function (result) {
                $(result).each(function (index, value) {

                    $("ul.navbar-nav.Categories").append("<li class='nav-item " + index + "'> </li>");
                    $("ul.navbar-nav.Categories li.nav-item." + index).append("<a class='nav-link' href='/Categories/Details/?id=" + value.id + "'>" + value.title + "</a>");
                });
             
            }
        });

   

});

/*Soccer Events - Json */
$(function () {

    $("div.Searchline.Score button.Submit").click(function () {
        $("div.socretable").html("<div class='containerjson row'></div>");
        var val = $("#LeagueSelect").find("option:selected").text();
        var id = $("#LeagueSelect").find("option:selected").val();

        var datefrom = $("#DateFrom").val();
        var dateto = $("#DateTo").val();
        $("div.socretable div.containerjson").addClass(val);
        var urlname = "https://apifootball.com/api/?action=get_events&from=" + datefrom + "&to=" + dateto + "&league_id=" + id + "&APIkey=df4ea49a08ed63529e46516ab69c3012e32ac721aafa8186a1d8c05fd61a5df6";
        $.ajax({

            url: urlname,
            dataType: 'json',
            type: 'get',
            cache: false,
            success: function (data) {
                if (data.length > 5) {
                    var newdata = data.slice(0, 5);
                }
                $(newdata).each(function (index, value) {
                    if (value.error === "404" || value.error === "201") {
                        $(".containerjson." + val).append("<h2 class='text-cneter text-danger mx-auto mt-2'>There are no such games!<h2>");
                    }
                    else {

                        $(".containerjson." + val).append("<div class='score col-sm-auto " + index + "'></div>");

                        $(".containerjson." + val + " div.score." + index).wrapInner("<table class='table'></table>").addClass("box");
                        $(".containerjson." + val + " div.score." + index + " table").append("<tr class='score '><td class='country_name'>" + value.country_name + "</td></tr>");
                        $(".containerjson." + val + " div.score." + index + " table").append("<tr class='score '><td class='league_name'>" + value.league_name + "</td></tr>");
                        $(".containerjson." + val + " div.score." + index + " table").append("<tr class='score '><td class='match_date'>" + value.match_date + "</td></tr>");
                        $(".containerjson." + val + " div.score." + index + " table").append("<tr class='score '><td class='match_hometeam_name'>" + value.match_hometeam_name +
                            "</td>" + "<td class='match_hometeam_score'>" + value.match_hometeam_score + "</td>" + "</td>" + "<td class='match_awayteam_name'>" + value.match_awayteam_name + "</td>" + "<td class='match_awayteam_score'>" + value.match_awayteam_score + "</td>" + "</tr>");
                        $("td.country_name").addClass("text-danger font-weight-bold");
                        $(".containerjson." + val + " div.score." + index).wrapInner("<div class='card'></div>");

                    }
                });



            }
        })

    });
});



