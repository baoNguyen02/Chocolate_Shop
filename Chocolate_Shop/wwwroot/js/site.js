// Write your Javascript code.

"use strict";
var con = new signalR.HubConnectionBuilder().withUrl("hub").build();
con.on("LoadAll", function () { location.reload(); });
con.start().then().catch(function (err) {
    return console.log(err.toString());
});