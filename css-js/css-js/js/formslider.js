$(document).ready(function(){
  $("form").submit(function(e) {
    e.preventDefault();
  });
  
  var clogin = $("#content-login");
  var cregister = $("#content-register");
  
  /* display the register page */
  $("#showregister").on("click", function(e){
    e.preventDefault();
    var newheight = cregister.height();
    $(cregister).css("display", "block");
    
    $(clogin).stop().animate({
      "left": "-880px"
    }, 800, function(){ /* callback */ });
    
    $(cregister).stop().animate({
      "left": "0px"
    }, 800, function(){ $(clogin).css("display", "none"); });
    
    $("#page").stop().animate({
      "height": newheight+"px"
    }, 550, function(){ /* callback */ });
  });
  
  /* display the login page */
  $("#showlogin").on("click", function(e){
    e.preventDefault();
    var newheight = clogin.height();
    $(clogin).css("display", "block");
    
    $(clogin).stop().animate({
      "left": "0px"
    }, 800, function() { /* callback */ });
    $(cregister).stop().animate({
      "left": "880px"
    }, 800, function() { $(cregister).css("display", "none"); });
    
    $("#page").stop().animate({
      "height": newheight+"px"
    }, 550, function(){ /* callback */ });
  });
});
///////////////////////////////////////////////////////////////////////////////////

$(document).ready(function(){
  $("form").submit(function(e) {
    e.preventDefault();
  });
  
  var clogin_new = $("#content-login-new");
  var cregister_new = $("#content-register-new");
  
  /* display the register page */
  $("#showregister").on("click", function(e){
    e.preventDefault();
    var newheight = cregister_new.height();
    $(cregister_new).css("display", "block");
    
    $(clogin_new).stop().animate({
      "left": "-880px"
    }, 800, function(){ /* callback */ });
    
    $(cregister_new).stop().animate({
      "left": "0px"
    }, 800, function(){ $(clogin_new).css("display", "none"); });
    
    $("#page1").stop().animate({
      "height": newheight+"px"
    }, 550, function(){ /* callback */ });
  });
  
  /* display the login page */
  $("#showlogin").on("click", function(e){
    e.preventDefault();
    var newheight = clogin_new.height();
    $(clogin_new).css("display", "block");
    
    $(clogin_new).stop().animate({
      "left": "0px"
    }, 800, function() { /* callback */ });
    $(cregister_new).stop().animate({
      "left": "880px"
    }, 800, function() { $(cregister_new).css("display", "none"); });
    
    $("#page1").stop().animate({
      "height": newheight+"px"
    }, 550, function(){ /* callback */ });
  });
});

$(document).ready(function(){
  $("form").submit(function(e) {
    e.preventDefault();
  });
  
  var clogin = $("#content-login-third");
  var cregister = $("#content-register-third");
  
  /* display the register page */
  $("#showregister").on("click", function(e){
    e.preventDefault();
    var newheight = cregister.height();
    $(cregister).css("display", "block");
    
    $(clogin).stop().animate({
      "left": "-880px"
    }, 800, function(){ /* callback */ });
    
    $(cregister).stop().animate({
      "left": "0px"
    }, 800, function(){ $(clogin).css("display", "none"); });
    
    $("#page2").stop().animate({
      "height": newheight+"px"
    }, 550, function(){ /* callback */ });
  });
  
  /* display the login page */
  $("#showlogin").on("click", function(e){
    e.preventDefault();
    var newheight = clogin.height();
    $(clogin).css("display", "block");
    
    $(clogin).stop().animate({
      "left": "0px"
    }, 800, function() { /* callback */ });
    $(cregister).stop().animate({
      "left": "880px"
    }, 800, function() { $(cregister).css("display", "none"); });
    
    $("#page2").stop().animate({
      "height": newheight+"px"
    }, 550, function(){ /* callback */ });
  });
});
