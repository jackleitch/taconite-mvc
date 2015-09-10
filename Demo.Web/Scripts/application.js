!function ($) {
  $(function() {

    window.prettyPrint && prettyPrint();


    $('.demoResetButton').each(function() {
      var $button = $(this);

      var exampleContent = $button.siblings('.demoActionWrapper').clone(true, true);
      $button.click(function () {
        $button.siblings('.demoActionWrapper').replaceWith(exampleContent.clone(true, true));
      });

    });


  });
}(window.jQuery)
