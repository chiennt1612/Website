////function check_user_email(email){
////  $('#check_user').html("... đang kiểm tra");
////  $.post("/ajax/check_user.php", {action : 'check-email', email : email}, function(data){
////    $('#check_user').html(data);
////    if($('#check_user').html().indexOf("có thể dùng đăng ký") > 0) $('#check_user').addClass("blue");
////    else $('#check_user').html("Tài khoản này đã tồn tại, nếu Quý khách quên mật khẩu vui lòng click <a href='/quen-mat-khau'>QUÊN MẬT KHẨU</a>");
////  });
////}

////function user_vote_review(e, t, id) {
////  $.post("/ajax/vote_product_review.php", {review_id: e, vote: t}, function (e) {
////    if ("error-not-login" == e)confirm("Để sử dụng chức năng này bạn cần đăng nhập") && (window.location = "/dang-nhap?return_url=" + window.location); else if ("error-has-voted" == e)alert("Bạn đã chọn rồi"); else if ("dislike" == t) {
////      if (message = "Bạn không đồng ý với ý kiến này. Bạn có thể muốn viết ý kiến của mình không ?", confirm(message)) {
////        var n = window.location, r = encodeURI(n).replace("=review", "=write-review");
////        -1 == r.search("=write-review") && (r += "?section=write-review"), window.location = r
////      }
////    } else{
////      alert("Cảm ơn bạn đã bày tỏ quan điểm");
////      $("#"+id).addClass("disable");
////      $("#"+id).removeAttr("onclick");
////    }
////  })
////}
////function check_user_captcha(captcha){
////  $.post("/ajax/check_user.php", {action : 'check-captcha', captcha : captcha}, function(data){
////    document.getElementById("check_captcha").value = data;
////  });
////}
////$.urlParam = function(name){
////  var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
////  if (results==null){
////    return null;
////  }
////  else{
////    return results[1] || 0;
////  }
////}



//////function countShoppingCart(e) {
//////  var t = document.getElementById("count_shopping_cart_store"),
//////      n = document.getElementById("total_shopping_cart_store");
//////  if (null == hura_read_cookie(e)) hura_create_cookie(e, "-", 30), "underfined" != t && null != t && (t.innerHTML = 0);
//////  else {
//////    var r = hura_read_cookie(e),
//////        o = r.split(",");
//////    if (number_product = o.length, "underfined" != t && null != t && (t.innerHTML = number_product - 1), "underfined" != n && null != n && $("#total_shopping_cart_store").length > 0) {
//////      for (var i = 0, c = 0; c < o.length; c++) item_content = o[c], item_content.length > 1 && (item_detail = item_content.split("-"), item_type = item_detail[0], pro_id = item_detail[1], pro_quantity = parseFloat(item_detail[2]), unit_price = parseFloat(item_detail[3]), i += unit_price * pro_quantity);
//////      n.innerHTML = writeStringToPrice(i + "");
      
//////    }
    
//////    $("#id_cart1,#id_cart_mobile1").html($("#count_shopping_cart_store").html());
//////    $("#id_cart_mobile").html($("#count_shopping_cart_store").html());
//////  }
//////}
////function user_like_content(e, t, n) {
////  $.post("/ajax/user_like.php", {
////    item_id: e,
////    content_type: t
////  }, function(e) {
////    "error-not-login" == e ? $("#user_like").html("Để sử dụng chức năng này bạn cần đăng nhập. Click OK để đăng nhập") && ($('#frm-login-register').modal('show')) : "error" == e ? $("#user_like,#user_like_1").html("Sản phẩm đã được đưa vào danh sách yêu thích của bạn") : $("#user_like,#user_like_1").html(" Sản phẩm ưa thích đã được thêm thành công!")
////  })
////}

////function user_like_check_button(e, t, n) {
////  $.post("/ajax/user_like.php", {
////    item_id: e,
////    content_type: t
////  }, function(e) {
////    if("error" == e){
////      $(".addtowish").hide();
////      $(".addtowish_unlike").show();
////    }
////  })
////}

////function removeFavoritePro_fixed(pro_id){
////  if(confirm('Bạn chắc chắn muốn xóa sản phẩm này khỏi danh sách đang lưu ?')){
////    $.post("/ajax/user_like_manager.php", {action: 'remove-like', type : 'pro', pro_id : pro_id}, function(){
////      $(".addtowish").show();
////      $(".addtowish_unlike").hide();
////    });
////  }
////}

////function countSavePro(){
////  $.post("/ajax/user_like_manager.php", {action: 'count-total-like', type : 'pro'}, function(data){
////    $(".checkfa").html(data);
////  });
  
////}

////$(document).ready(function(){
////  countSavePro();
////  //$("#menu").addClass({ "max-height": $(window).height() - $(".navbar-header").height() + "px !important" });
////});




////function validateRegister()
////{
////  var _str = "";
  
////  if($("#full_name").val() == "")
////  {
////    _str += "Bạn chưa điền họ và tên\n";
////  }
////  if($("#regemail").val() == "")
////  {
////    _str += "Bạn chưa điền email\n";
////  }
////  if($("#regpassword").val() == "")
////  {
////    _str += "Bạn chưa điền password\n";
////  }
////  if($("#repassword").val() == "")
////  {
////    _str += "Bạn chưa điền repassword\n";
////  }
////  if($("#regpassword").val().trim() != $("#repassword").val().trim())
////  {
////    _str += "Mật khẩu không khớp\n";
////  }
  
////  var captcha = document.getElementById('register_captcha').value;
////  if(captcha=='') _str+="Bạn chưa nhập mã kiểm tra"
////  else {
////    var check_captcha = $("#check_captcha").val();
////    if(check_captcha!='') _str+=check_captcha;
////  }
  
////  if(_str != ''){
    
////    alert(_str);
////    return false;
////  }
////  else{
////    return true;
////  }
////}







$(document).ready(function(){
  $("#menu-affix").affix({
    offset: { 
      top: 40
    }
  });
  
  $(".nav li.dropdown .titleDrop").click(function(){
  	$(this).parent().find(".dropdown-menu").toggle();
  });
});

function searchHeader(){
  if($("#qheader").val() == ''){
    $("#qheader").focus();
  }else{
      var a = $("#qheader").val();
      if (a.trim().length > 2) {
          document.forms["frmSearchByKeywordMobile"].Keyword.value = a;
          document.forms["frmSearchByKeywordMobile"].action = "/Categories/Search/" + 1;
          document.forms["frmSearchByKeywordMobile"].submit();
      }
      else
      {
          $("#qheader").select();
      }
  }
}

$("#qheader").keyup(function(b){
    if (b.keyCode != 38 && b.keyCode != 40) {
        inputString = $(this).val();
        if (inputString.trim().length > 2) {
          $(".autocomplete-suggestions").show();
            $(".autocomplete-suggestions").load("/Categories/QuickSearch?template=header&q="+encodeURIComponent(inputString));
        }else  {
          $(".autocomplete-suggestions").hide();
          count_select=0;
        }
      }

    if (b.keyCode == 40) {
        count_select++;
        curr_element = $(".autocomplete-suggestion:nth-child("+count_select+")");

        curr_text = curr_element.find(".suggest_link").text();
        $("#qheader").val(curr_text);
        $(".autocomplete-suggestion").removeClass("selected");
        $(curr_element).addClass("selected");
        
    }

    if (b.keyCode == 38 && count_select > 1) {
        count_select--;
        curr_element = $(".autocomplete-suggestion:nth-child("+count_select+")");
        curr_text = curr_element.find(".suggest_link").text();
        $("#qheader").val(curr_text);
        $(".autocomplete-suggestion").removeClass("selected");
        $(curr_element).addClass("selected");
    }
});