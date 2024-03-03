
const testArray = {
    SA01: {
        name: "الرياض",
        description: "5"
    },
    SA02: {
        name: "مكة المكرمة",
        description: "0"
    },
    SA03: {
        name: "المدينة المنورة",
        description: "0"
    },
    SA04: {
        name: "المنطقة الشرقية",
        description: "0"
    },
    SA05: {
        name: "القصيم",
        description: "0"
    },
    SA06: {
        name: "حائل",
        description: "0"
    },
    SA07: {
        name: "تبوك",
        description: "0"
    },
    SA08: {
        name: "الحدود الشمالية",
        description: "0"
    },
    SA09: {
        name: "جازان",
        description: "0"
    },
    SA10: {
        name: "نجران",
        description: "0"
    },
    SA11: {
        name: "الباحة",
        description: "0"
    },
    SA12: {
        name: "الجوف",
        description: "0"
    },
    SA14: {
        name: "عسير",
        description: "0"
    }
}

function getAllCountry() {


    $.get("getAllArea", function (data) {
        for (let i = 0; i < data.length; i++) {
            switch (data[i].countrySymbol) {
                case "SA01":
                    testArray["SA01"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA02":
                    testArray["SA02"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA03":
                    testArray["SA03"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA04":
                    testArray["SA04"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA05":
                    testArray["SA05"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA06":
                    testArray["SA06"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA07":
                    testArray["SA07"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA08":
                    testArray["SA08"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA09":
                    testArray["SA09"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA10":
                    testArray["SA10"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA11":
                    testArray["SA11"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA12":
                    testArray["SA12"].description = data[i].countryWithProjects.projectNumber
                    break;
                case "SA14":
                    testArray["SA14"].description = data[i].countryWithProjects.projectNumber
                    break;
                

                default:
                // code block
            }
        }

    });
    const newArray = testArray;
    console.log(newArray);

    return newArray;
}




var simplemaps_countrymap_mapdata = {

    main_settings: {
        //General settings
        width: "responsive", //'700' or 'responsive'
        background_color: "#FFFFFF",
        background_transparent: "yes",
        border_color: "#ffffff",

        //State defaults
        state_description: "State description",
        state_color: "#14623a",
        state_hover_color: "#104027",
        state_url: "",
        border_size: 1.5,
        all_states_inactive: "no",
        all_states_zoomable: "yes",

        //Location defaults
        location_description: "(العاصمة)",
        location_url: "",
        location_color: "#FF0067",
        location_opacity: 0.8,
        location_hover_opacity: 1,
        location_size: 25,
        location_type: "square",
        location_image_source: "frog.png",
        location_border_color: "#FFFFFF",
        location_border: 2,
        location_hover_border: 2.5,
        all_locations_inactive: "no",
        all_locations_hidden: "no",

        //Label defaults
        label_color: "#ffffff",
        label_hover_color: "#ffffff",
        label_size: 16,
        label_font: "Arial",
        label_display: "auto",
        label_scale: "yes",
        hide_labels: "yes",
        hide_eastern_labels: "no",

        //Zoom settings
        zoom: "yes",
        manual_zoom: "no",
        back_image: "no",
        initial_back: "no",
        initial_zoom: "-1",
        initial_zoom_solo: "no",
        region_opacity: 1,
        region_hover_opacity: 0.6,
        zoom_out_incrementally: "yes",
        zoom_percentage: 0.99,
        zoom_time: 0.5,

        //Popup settings
        popup_color: "white",
        popup_opacity: 0.9,
        popup_shadow: 1,
        popup_corners: 5,
        popup_font: "12px/1.5 Verdana, Arial, Helvetica, sans-serif",
        popup_nocss: "no",

        //Advanced settings
        div: "map",
        auto_load: "yes",
        url_new_tab: "no",
        images_directory: "default",
        fade_time: 0.1,
        link_text: "View Website",
        popups: "detect",
        state_image_url: "",
        state_image_position: "",
        location_image_url: ""
    },
    state_specific: getAllCountry(),
    locations: {
        "0": {
            name: "الرياض",
            lat: "24.653837",
            lng: "46.715683"
        }
    },
    labels: {
        SA01: {
            name: "الرياض",
            parent_id: "SA01"
        },
        SA02: {
            name: "مكة المكرمة",
            parent_id: "SA02"
        },
        SA03: {
            name: "المدينة المنورة",
            parent_id: "SA03"
        },
        SA04: {
            name: "المنطقة الشرقية",
            parent_id: "SA04"
        },
        SA05: {
            name: "القصيم",
            parent_id: "SA05"
        },
        SA06: {
            name: "حائل",
            parent_id: "SA06"
        },
        SA07: {
            name: "تبوك",
            parent_id: "SA07"
        },
        SA08: {
            name: "الحدود الشمالية",
            parent_id: "SA08"
        },
        SA09: {
            name: "جازان",
            parent_id: "SA09"
        },
        SA10: {
            name: "نجران",
            parent_id: "SA10"
        },
        SA11: {
            name: "الباحة",
            parent_id: "SA11"
        },
        SA12: {
            name: "الجوف",
            parent_id: "SA12"
        },
        SA14: {
            name: "عسير",
            parent_id: "SA14"
        }
    },
    legend: {
        entries: []
    },
    regions: {}
};
