
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
   
    $("div.Searchline.Score button.Submit").click(function () {
        $("div.socretable").html("<div class='containerjson row'></div>");
        var val = $("#LeagueSelect").find("option:selected").text();
        var id = $("#LeagueSelect").find("option:selected").val();
       
        var datefrom = $("#DateFrom").val();
        var dateto = $("#DateTo").val();
        $("div.socretable div.containerjson").addClass(val);
       // var elements = $(".containerjson").map(function () { return this.id; });
        //var country = $.makeArray(elements);
      //  country.forEach(function (val, index) {
            //var id = 0;
            //switch (val) {
            //    case "England":
            //        id = "62";
            //        break;
            //    case "Italy":
            //        id = "79";
            //        break;
            //    case "Spain":
            //        id = "109";
            //        break;
            //    case "Germany":
            //        id = "117";
            //        break;
            //    case "France":
            //        id = "127";
            //        break;
            //    case "Israel":
            //        id = "437";
            //        break;
            //    case "Portugal":
            //        id = "150";
            //        break;
            //    case "Belgium":
            //        id = "144";
            //        break;
            //    case "Denmark":
            //        id = "284";
            //        break;
            //    default:
            //        id = "62";
            //}

        var urlname = "https://apifootball.com/api/?action=get_events&from=" + datefrom + "&to=" + dateto + "&league_id=" + id + "&APIkey=df4ea49a08ed63529e46516ab69c3012e32ac721aafa8186a1d8c05fd61a5df6";
            $.ajax({
                
                url: urlname,
                dataType: 'json',
                type: 'get',
                cache: false,
                success: function (data) {
                    var newdata = data.slice(0, 5);
                    $(newdata).each(function (index, value) {
                        if (value.error == "404" || value.error == "201") {
                            $(".containerjson." + val).append("<h2 class='text-cneter text-danger mx-auto mt-2'>There are no such games!<h2>");
                        }
                        else{
                           
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

