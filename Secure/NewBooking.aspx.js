// JScript File

<script language="JavaScript">

function init_shortcuts() {


shortcut.add("b",function() {
	getElementById("NewBookingFormView$InsertButton").submit();},{
	'disable_in_input':true});

shortcut.add("ESC",function() {
	window.opener='x';window.close();},{
	'disable_in_input':false});


}

</script>
