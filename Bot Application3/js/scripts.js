$(document).ready(function() {
    
    $('#map').mapster({
        fillOpacity: 1,
        mapKey: 'data-key',
        showToolTip: true,
        areas:  [{
            key: "17-18",
            fillColor: 'e77336',
            stroke: true,
            selected: true
        }
        ]
    });

    $('<div style="padding: 3px 6px; border: solid 1px #F4281B; color: #E77336;">').appendTo('map').html('<span style="background: #E77336; width: 10px; height: 10px; margin-right: 10px; display: inline-block;"></span>星巴克').css({position:'absolute',
                                                 top:'3%',
                                                 left:'5%'});
});