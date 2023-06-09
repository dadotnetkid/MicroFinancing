// Class definition
var KTWizardDemo = function () {
    // Base elements
    var wizardEl;
    var formEl;
    var validator;
    var wizard;
    
    // Private functions
    var initWizard = function () {
        // Initialize form wizard
        wizard = new KTWizard('kt_wizard_v2', {
            startStep: 1,
            clickableSteps: false 
        });

        // Validation before going to next page
        wizard.on('beforeNext', function(wizardObj) {
            if (validator.form() !== true) {
                wizardObj.stop();  // don't go to the next step
            }
        });

        wizard.on('beforePrev', function(wizardObj) {
			if (validator.form() !== true) {
				wizardObj.stop();  // don't go to the next step
			}
		});

        // Change event
        wizard.on('change', function(wizard) {
            KTUtil.scrollTop();    
        });
    }

    var initValidation = function () {
        validator = formEl.validate({
            // Validate only visible fields
            ignore: ":hidden",

            // Submit valid form
            submitHandler: function (form) {
            }
        });
    }

    var initSubmit = function () {

        var btn = formEl.find('[data-ktwizard-type="action-submit"]');

        btn.on('click', function (e) {
            e.preventDefault();

            validator = formEl.validate();

            if (validator.form()) {

                // See: src\js\framework\base\app.js
                KTApp.progress(btn);
                //KTApp.block(formEl);

                formEl.submit();
            }
        });
    }

    return {
        // public functions
        init: function() {
            wizardEl = KTUtil.get('kt_wizard_v2');
            formEl = $('#kt_form');

            initWizard(); 
            initValidation();
            initSubmit();
        }
    };
}();

jQuery(document).ready(function() {    
    KTWizardDemo.init();
});