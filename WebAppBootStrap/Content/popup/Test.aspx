<%@ Page Title="popup test" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebAppBS.Content.popup.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/magnific-popup.css" rel="stylesheet" />
    <script src="js/jquery.magnific-popup.js"></script>
    <script>
        $(function() {
            $('.image-popup-vertical-fit').magnificPopup({
                type: 'image',
                closeOnContentClick: true,
                mainClass: 'mfp-img-mobile',
                image: {
                    verticalFit: true
                }

            });

            $('.image-popup-fit-width').magnificPopup({
                type: 'image',
                closeOnContentClick: true,
                image: {
                    verticalFit: false
                }
            });

            $('.image-popup-no-margins').magnificPopup({
                type: 'image',
                closeOnContentClick: true,
                closeBtnInside: false,
                fixedContentPos: true,
                mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
                image: {
                    verticalFit: true
                },
                zoom: {
                    enabled: true,
                    duration: 300 // don't foget to change the duration also in CSS
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">pic test</div>
        <div class="panel-body">
            <a class="image-popup-vertical-fit" href="http://farm9.staticflickr.com/8241/8589392310_7b6127e243_b.jpg" title="Caption. Can be aligned to any side and contain any HTML.">
                <img src="http://farm9.staticflickr.com/8241/8589392310_7b6127e243_s.jpg" width="75" height="75"/>
            </a>
            <a class="image-popup-fit-width" href="http://farm9.staticflickr.com/8379/8588290361_ecf8c27021_b.jpg" title="This image fits only horizontally.">
                <img src="http://farm9.staticflickr.com/8379/8588290361_ecf8c27021_s.jpg" width="75" height="75"/>
            </a>
            <a class="image-popup-no-margins" href="http://farm4.staticflickr.com/3721/9207329484_ba28755ec4_o.jpg">
                <img src="http://farm4.staticflickr.com/3721/9207329484_ba28755ec4_o.jpg" width="107" height="75"/>
            </a>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">pic list</div>
        <div class="panel-body">
            <script>
                $(document).ready(function () {
                    $('.popup-gallery').magnificPopup({
                        delegate: 'a',
                        type: 'image',
                        tLoading: 'Loading image #%curr%...',
                        mainClass: 'mfp-img-mobile',
                        gallery: {
                            enabled: true,
                            navigateByImgClick: true,
                            preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
                        },
                        image: {
                            tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
                            titleSrc: function (item) {
                                return item.el.attr('title') + '<small>by Marsel Van Oosten</small>';
                            }
                        }
                    });
                });
            </script>
            <div class="popup-gallery">
                <a href="http://farm9.staticflickr.com/8242/8558295633_f34a55c1c6_b.jpg" title="The Cleaner">
                    <img src="http://farm9.staticflickr.com/8242/8558295633_f34a55c1c6_s.jpg" width="75" height="75">
                </a>
                <a href="http://farm9.staticflickr.com/8382/8558295631_0f56c1284f_b.jpg" title="Winter Dance">
                    <img src="http://farm9.staticflickr.com/8382/8558295631_0f56c1284f_s.jpg" width="75" height="75">
                </a>
                <a href="http://farm9.staticflickr.com/8225/8558295635_b1c5ce2794_b.jpg" title="The Uninvited Guest">
                    <img src="http://farm9.staticflickr.com/8225/8558295635_b1c5ce2794_s.jpg" width="75" height="75">
                </a>
                <a href="http://farm9.staticflickr.com/8383/8563475581_df05e9906d_b.jpg" title="Oh no, not again!"><img src="http://farm9.staticflickr.com/8383/8563475581_df05e9906d_s.jpg" width="75" height="75"></a>
                <a href="http://farm9.staticflickr.com/8235/8559402846_8b7f82e05d_b.jpg" title="Swan Lake"><img src="http://farm9.staticflickr.com/8235/8559402846_8b7f82e05d_s.jpg" width="75" height="75"></a>
                <a href="http://farm9.staticflickr.com/8235/8558295467_e89e95e05a_b.jpg" title="The Shake"><img src="http://farm9.staticflickr.com/8235/8558295467_e89e95e05a_s.jpg" width="75" height="75"></a>
                <a href="http://farm9.staticflickr.com/8378/8559402848_9fcd90d20b_b.jpg" title="Who's that, mommy?"><img src="http://farm9.staticflickr.com/8378/8559402848_9fcd90d20b_s.jpg" width="75" height="75"></a>
              </div>
        </div>
    </div>
    
    <div class="panel panel-default">
        <div class="panel-heading">pic zoom</div>
        <div class="panel-body">
            <script>
                $(document).ready(function () {
                    $('.zoom-gallery').magnificPopup({
                        delegate: 'a',
                        type: 'image',
                        closeOnContentClick: false,
                        closeBtnInside: false,
                        mainClass: 'mfp-with-zoom mfp-img-mobile',
                        image: {
                            verticalFit: true,
                            titleSrc: function (item) {
                                return item.el.attr('title') + ' &middot; <a class="image-source-link" href="' + item.el.attr('data-source') + '" target="_blank">image source</a>';
                            }
                        },
                        gallery: {
                            enabled: true
                        },
                        zoom: {
                            enabled: true,
                            duration: 300, // don't foget to change the duration also in CSS
                            opener: function (element) {
                                return element.find('img');
                            }
                        }

                    });
                });
            </script>
            <div class="zoom-gallery">
                <!--

                Width/height ratio of thumbnail and the main image must match to avoid glitches.

                If ratios are different, you may add CSS3 opacity transition to the main image to make the change less noticable.

                    -->
                <a href="http://farm4.staticflickr.com/3763/9204547649_0472680945_o.jpg" data-source="http://500px.com/photo/32736307" title="Into The Blue" style="width:193px;height:125px;">
                    <img src="http://farm4.staticflickr.com/3763/9204547649_7de96ee188_t.jpg" width="193" height="125">
                </a>
                <a href="http://farm3.staticflickr.com/2856/9207329420_7f2a668b06_o.jpg" data-source="http://500px.com/photo/32554131" title="Light Sabre" style="width:82px;height:125px;">
                    <img src="http://farm3.staticflickr.com/2856/9207329420_e485948b01_t.jpg" width="82px" height="125">
                </a>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">video map</div>
        <div class="panel-body">
            <script>
                $(document).ready(function () {
                    $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
                        disableOn: 700,
                        type: 'iframe',
                        mainClass: 'mfp-fade',
                        removalDelay: 160,
                        preloader: false,
                        fixedContentPos: false
                    });
                });
            </script>
            <a class="popup-youtube" href="http://www.youtube.com/watch?v=0O2aH4XLbto">Open YouTube video</a>
            <br/>
            <a class="popup-vimeo" href="https://vimeo.com/45830194">Open Vimeo video</a>
            <br/>
            <a class="popup-gmaps" href="https://maps.google.com/maps?q=221B+Baker+Street,+London,+United+Kingdom&amp;hl=en&amp;t=v&amp;hnear=221B+Baker+St,+London+NW1+6XE,+United+Kingdom">Open Google Map</a>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">dialog</div>
        <div class="panel-body">
            <style>
                /* Styles for dialog window */
                #small-dialog {
                    background: white;
                    padding: 20px 30px;
                    text-align: left;
                    max-width: 400px;
                    margin: 40px auto;
                    position: relative;
                }
                /**
                 * Fade-zoom animation for first dialog
                 */

                /* start state */
                .my-mfp-zoom-in .zoom-anim-dialog {
                    opacity: 0;

                    -webkit-transition: all 0.2s ease-in-out; 
                    -moz-transition: all 0.2s ease-in-out; 
                    -o-transition: all 0.2s ease-in-out; 
                    transition: all 0.2s ease-in-out; 



                    -webkit-transform: scale(0.8); 
                    -moz-transform: scale(0.8); 
                    -ms-transform: scale(0.8); 
                    -o-transform: scale(0.8); 
                    transform: scale(0.8); 
                }

                /* animate in */
                .my-mfp-zoom-in.mfp-ready .zoom-anim-dialog {
                    opacity: 1;

                    -webkit-transform: scale(1); 
                    -moz-transform: scale(1); 
                    -ms-transform: scale(1); 
                    -o-transform: scale(1); 
                    transform: scale(1); 
                }

                /* animate out */
                .my-mfp-zoom-in.mfp-removing .zoom-anim-dialog {
                    -webkit-transform: scale(0.8); 
                    -moz-transform: scale(0.8); 
                    -ms-transform: scale(0.8); 
                    -o-transform: scale(0.8); 
                    transform: scale(0.8); 

                    opacity: 0;
                }

                /* Dark overlay, start state */
                .my-mfp-zoom-in.mfp-bg {
                    opacity: 0;
                    -webkit-transition: opacity 0.3s ease-out; 
                    -moz-transition: opacity 0.3s ease-out; 
                    -o-transition: opacity 0.3s ease-out; 
                    transition: opacity 0.3s ease-out;
                }
                /* animate in */
                .my-mfp-zoom-in.mfp-ready.mfp-bg {
                    opacity: 0.8;
                }
                /* animate out */
                .my-mfp-zoom-in.mfp-removing.mfp-bg {
                    opacity: 0;
                }

                /**
                 * Fade-move animation for second dialog
                 */

                /* at start */
                .my-mfp-slide-bottom .zoom-anim-dialog {
                    opacity: 0;
                    -webkit-transition: all 0.2s ease-out;
                    -moz-transition: all 0.2s ease-out;
                    -o-transition: all 0.2s ease-out;
                    transition: all 0.2s ease-out;

                    -webkit-transform: translateY(-20px) perspective( 600px ) rotateX( 10deg );
                    -moz-transform: translateY(-20px) perspective( 600px ) rotateX( 10deg );
                    -ms-transform: translateY(-20px) perspective( 600px ) rotateX( 10deg );
                    -o-transform: translateY(-20px) perspective( 600px ) rotateX( 10deg );
                    transform: translateY(-20px) perspective( 600px ) rotateX( 10deg );

                }

                /* animate in */
                .my-mfp-slide-bottom.mfp-ready .zoom-anim-dialog {
                    opacity: 1;
                    -webkit-transform: translateY(0) perspective( 600px ) rotateX( 0 ); 
                    -moz-transform: translateY(0) perspective( 600px ) rotateX( 0 ); 
                    -ms-transform: translateY(0) perspective( 600px ) rotateX( 0 ); 
                    -o-transform: translateY(0) perspective( 600px ) rotateX( 0 ); 
                    transform: translateY(0) perspective( 600px ) rotateX( 0 ); 
                }

                /* animate out */
                .my-mfp-slide-bottom.mfp-removing .zoom-anim-dialog {
                    opacity: 0;

                    -webkit-transform: translateY(-10px) perspective( 600px ) rotateX( 10deg ); 
                    -moz-transform: translateY(-10px) perspective( 600px ) rotateX( 10deg ); 
                    -ms-transform: translateY(-10px) perspective( 600px ) rotateX( 10deg ); 
                    -o-transform: translateY(-10px) perspective( 600px ) rotateX( 10deg ); 
                    transform: translateY(-10px) perspective( 600px ) rotateX( 10deg ); 
                }

                /* Dark overlay, start state */
                .my-mfp-slide-bottom.mfp-bg {
                    opacity: 0;

                    -webkit-transition: opacity 0.3s ease-out; 
                    -moz-transition: opacity 0.3s ease-out; 
                    -o-transition: opacity 0.3s ease-out; 
                    transition: opacity 0.3s ease-out;
                }
                /* animate in */
                .my-mfp-slide-bottom.mfp-ready.mfp-bg {
                    opacity: 0.8;
                }
                /* animate out */
                .my-mfp-slide-bottom.mfp-removing.mfp-bg {
                    opacity: 0;
                }
            </style>
            <script>
                $(document).ready(function () {
                    $('.popup-with-zoom-anim').magnificPopup({
                        type: 'inline',

                        fixedContentPos: false,
                        fixedBgPos: true,

                        overflowY: 'auto',

                        closeBtnInside: true,
                        preloader: false,

                        midClick: true,
                        removalDelay: 300,
                        mainClass: 'my-mfp-zoom-in'
                    });

                    $('.popup-with-move-anim').magnificPopup({
                        type: 'inline',

                        fixedContentPos: false,
                        fixedBgPos: true,

                        overflowY: 'auto',

                        closeBtnInside: true,
                        preloader: false,

                        midClick: true,
                        removalDelay: 300,
                        mainClass: 'my-mfp-slide-bottom'
                    });
                });
            </script>
            <a class="popup-with-zoom-anim" href="#small-dialog">Open with fade-zoom animation</a>
            <br/>
            <a class="popup-with-move-anim" href="#small-dialog">Open with fade-slide animation</a>
            <br />
            <!-- dialog itself, mfp-hide class is required to make dialog hidden -->
            <div id="small-dialog" class="zoom-anim-dialog mfp-hide">
	            <h1>Dialog example</h1>
	            <p>This is dummy copy. It is not meant to be read. It has been placed here solely to demonstrate the look and feel of finished, typeset text. Only for show. He who searches for meaning here will be sorely disappointed.</p>
            </div>
        </div>
    </div>
    
    <div class="panel panel-default">
        <div class="panel-heading">Popup with form</div>
        <div class="panel-body">
            <style>
                .white-popup-block {
                  background: #FFF;
                  padding: 20px 30px;
                  text-align: left;
                  max-width: 650px;
                  margin: 40px auto;
                  position: relative;
                }
            </style>
            <script>
                $(document).ready(function () {
                    $('.popup-with-form').magnificPopup({
                        type: 'inline',
                        preloader: false,
                        focus: '#name',
                        modal:false,
                        // When elemened is focused, some mobile browsers in some cases zoom in
                        // It looks not nice, so we disable it:
                        callbacks: {
                            beforeOpen: function () {
                                if ($(window).width() < 700) {
                                    this.st.focus = false;
                                } else {
                                    this.st.focus = '#name';
                                }
                            }
                        }
                    });
                });
            </script>
            <!-- link that opens popup -->
            <a class="popup-with-form" href="#test-form">Open form</a>

            <!-- form itself -->
            <div id="test-form" class="white-popup-block mfp-hide">
	            <h1>Form</h1>
	             <table class="table" style="width:500px">
	                 <tr>
	                     <td>name</td>
                         <td><input type="text" class="form-control"/></td>
	                 </tr>
                     <tr>
	                     <td>age</td>
                         <td><input type="text" class="form-control"/></td>
	                 </tr>
	             </table>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">ajax test</div>
        <div class="panel-body">
            <script>
                $(document).ready(function () {

                    $('.simple-ajax-popup-align-top').magnificPopup({
                        type: 'ajax',
                        alignTop: true,
                        overflowY: 'scroll' // as we know that popup content is tall we set scroll overflow by default to avoid jump
                    });

                    $('.simple-ajax-popup').magnificPopup({
                        type: 'ajax'
                    });

                });
            </script>                                     
            <a class="simple-ajax-popup-align-top" href="Test.aspx">Load content via ajax</a><br>
            <a class="simple-ajax-popup" href="http://dimsemenov.com/plugins/magnific-popup/site-assets/ajax/test-ajax-2.html">Load another content via ajax</a>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">Modal</div>
        <div class="panel-body">
            <script>
                $(function () {
                    $('.popup-modal').magnificPopup({
                        type: 'inline',
                        preloader: false,
                        focus: '#username',
                        modal: true
                    });
                    $(document).on('click', '.popup-modal-dismiss', function (e) {
                        e.preventDefault();
                        $.magnificPopup.close();
                    });
                });
            </script>
            <a class="popup-modal" href="#test-modal">Open modal</a>

            <div id="test-modal" class="white-popup-block mfp-hide">
	            <h1>Modal dialog</h1>
	            <p>You won't be able to dismiss this by usual means (escape or
		            click button), but you can close it programatically based on
		            user choices or actions.</p>
	            <p><a class="popup-modal-dismiss" href="#">Dismiss</a></p>
            </div>
        </div>
    </div>
</asp:Content>
