function showSwalAndSubmitForm(formSelector, tableSelector, successMessage, urlAddress) {
    $(formSelector).on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        // Create a new FormData object
        var formData = new FormData(this);
        var file = formData.get('Image'); // Get the uploaded file

        // If no file is uploaded, submit the form directly
        if (!file || file.size === 0) {
            submitForm(formData, urlAddress, successMessage, tableSelector);
            return;
        }

        // Define header signatures for valid image formats
        var validHeaders = [
            [0xFF, 0xD8, 0xFF, 0xE0], // JPEG
            [0x89, 0x50, 0x4E, 0x47], // PNG
            [0xFF, 0xD8, 0xFF] // JPG
        ];

        var fileReader = new FileReader();
        fileReader.onload = function (event) {
            var headerBytes = new Uint8Array(event.target.result).subarray(0, 4);

            var isValidFormat = validHeaders.some(function (validHeader) {
                return validHeader.every(function (byte, index) {
                    return byte === headerBytes[index];
                });
            });

            if (!isValidFormat) {
                Swal.fire({
                    title: 'خطأ!',
                    text: 'ملف الصورة غير صالح.',
                    icon: 'error',
                    confirmButtonColor: '#d33',
                    confirmButtonText: 'OK'
                });
                return; // Stop further execution
            }

            // If the file is valid, submit the form
            submitForm(formData, urlAddress, successMessage, tableSelector);
        };

        // Read the file as an ArrayBuffer
        fileReader.readAsArrayBuffer(file);
    });

    // Reset the form on Discard button click
    $(formSelector).on('click', '[type="reset"]', function () {
        document.querySelector(formSelector).reset();
    });
}

// Separate function to submit the form
function submitForm(formData, urlAddress, successMessage, tableSelector) {
    $.ajax({
        url: urlAddress,
        type: 'POST',
        cache: false,
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            Swal.fire({
                title: 'تم!',
                text: successMessage,
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'حسنا'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Reload data and perform other actions
                    $(tableSelector).DataTable().ajax.reload(null, false);
                    var discardButton = document.querySelector('.btn-discard');
                    if (discardButton) {
                        discardButton.click();
                    }
                }
            });
        },
        error: function (xhr, status, error) {
            // Handle the error here
            if (xhr.status === 500) {
                console.error('Resource not found.');
                console.error(urlAddress);
            } else {
                console.error('An error occurred:', error);
            }
        },
    });
}


function ShowAnyModal(className, modal, urlAddress, showModal, modalName) {
    $(document).on("click", className, function () {
        let modelId = $(this).data('id');
        $.ajax({
            url: "/" + urlAddress + "/" + modelId,
            type: 'GET',
            cache: false,
            success: function (result) {
                $(".modal-backdrop.show").remove();
                $(showModal).html(result);
                $(modalName).modal("show");
            }

        });
        return false;
    });


}



$(document).on("click", ".btn-closee, .btn-hide", function () {
    // Trigger the modal's close functionality
    $('#kt_modal_details').modal('hide');
    $('#kt_modal_details').remove();

    $('#kt_modal_update').modal('hide');
    $('#kt_modal_update').remove();
});




// Add a click event listener to the buttons with class 'btn-status'

function statusHandeller(tableSelector, urlAddress) {

    $(tableSelector).on('click', '.btn-status', function (event) {
        event.preventDefault(); // Prevent default button behavior
        var btn = $(this); // Save the reference to the button that was clicked
        var Id = btn.data('id'); // Get the chef ID from the data attribute

        // Make the AJAX request with cache: false
        $.ajax({
            url: urlAddress + Id,
            type: 'GET',
            cache: false, // Prevent caching of the response
            success: function (data) {


                // Check the current class and update it accordingly
                if (btn.hasClass('btn-danger')) {
                    btn.removeClass('btn-danger').addClass('btn-success');

                    // Update the button class and text based on the response
                    var newStatus = 'عام';
                    btn.text(newStatus);

                } else if (btn.hasClass('btn-success')) {
                    btn.removeClass('btn-success').addClass('btn-danger');

                    // Update the button class and text based on the response
                    var newStatus = 'خاص';
                    btn.text(newStatus);
                }
            },
            error: function () {
                alert('An error occurred while updating the status.');
            }
        });
    });
}

// Event handler for Delete button
function deleteHandeller(tableSelector, urlAddress, successMessage) {

    $(tableSelector).on('click', '.btn-delete', function (e) {
        e.preventDefault();
        var eventId = $(this).data('id');

        // Show SweetAlert confirmation
        Swal.fire({
            title: 'هل انت متأكد ؟',
            text: "لا يمكنك التراجع هن هذا الاجراء",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'نعم, قم بحذفه',
            cancelButtonText: 'إلغاء'
        }).then((result) => {
            if (result.isConfirmed) {
                // If user confirms, delete the row and send request to the server
                var deleteUrl = urlAddress + eventId; // Replace with your actual Delete route URL
                $.ajax({
                    url: deleteUrl,
                    type: 'POST',
                    success: function (data) {
                        // On successful deletion, hide the row and show success message
                        Swal.fire({
                            title: 'تم الحذف بنجاح',
                            text: successMessage,
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'حسنا'
                        }).then(function () {
                            // Hide the deleted row
                            $(e.target).closest('tr').hide();
                        });
                    },
                    error: function (error) {
                        // Handle any error that may occur during the request
                        console.error(error);
                    }
                });
            }
        });
    });

}


function updateHandeller(tableSelector, postUrlAddress, getUrlAddress,successMessage) {
    $(tableSelector).on('click', '.btn-update', function () {
        // Load the modal content using AJAX
        var Id = $(this).data('id');
        var UrlUpdate = getUrlAddress + Id;

        $.get(UrlUpdate, function (data) {
            $(".modal-backdrop.show").remove();
            $("#ShowModal").html(data);
            $("#kt_modal_update").modal("show");

            // Attach an event listener to the submit button inside the modal
            $("#kt_modal_update").on('submit', 'form', function (e) {
                e.preventDefault(); // Prevent default form submission

                var formData = new FormData(this);

                var file = formData.get('Image'); // Get the uploaded file
                if (file.size !== 0) {
                    // Define header signatures for valid image formats
                    var validHeaders = [
                        [0xFF, 0xD8, 0xFF, 0xE0], // JPEG
                        [0x89, 0x50, 0x4E, 0x47], // PNG
                        [0xFF, 0xD8, 0xFF] // JPG
                    ];

                    var fileReader = new FileReader();
                    fileReader.onload = function (event) {
                        var headerBytes = new Uint8Array(event.target.result).subarray(0, 4);

                        var isValidFormat = validHeaders.some(function (validHeader) {
                            return validHeader.every(function (byte, index) {
                                return byte === headerBytes[index];
                            });
                        });

                        if (!isValidFormat) {
                            Swal.fire({
                                title: 'Error',
                                text: 'Invalid image file format.',
                                icon: 'error',
                                confirmButtonColor: '#d33',
                                confirmButtonText: 'OK'
                            });
                            return; // Stop further execution
                        }

                        // Continue with AJAX request if image format is valid
                        $.ajax({
                            url: postUrlAddress, // Replace with your actual server URL to add/update the item
                            type: 'POST',
                            cache: false,
                            data: formData,
                            processData: false, // Important: prevent jQuery from processing the data
                            contentType: false, // Important: let the server handle the content type
                            success: function (data) {
                                Swal.fire({
                                    title: 'تم!',
                                    text: successMessage,
                                    icon: 'success',
                                    confirmButtonColor: '#3085d6',
                                    confirmButtonText: 'حسنا',
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        $('#kt_modal_update').modal('hide');
                                        $('#kt_modal_update').remove();
                                        $(tableSelector).DataTable().ajax.reload(null, false);
                                    }
                                });
                            },
                            error: function (xhr, status, error) {
                                // Handle the error here
                                if (xhr.status === 500) {
                                    console.error('Resource not found.');
                                    window.location.href = '/Error/500.html'; // Replace with the actual URL of your custom 404 page
                                } else {
                                    console.error('An error occurred:', error);
                                    window.location.href = '/Error/500.html';
                                }
                            },
                        });
                    };

                    // Read the file as an ArrayBuffer
                    fileReader.readAsArrayBuffer(file);
                } else {
                    // No image provided, continue with the AJAX request
                    $.ajax({
                        url: postUrlAddress, // Replace with your actual server URL to add/update the item
                        type: 'POST',
                        cache: false,
                        data: formData,
                        processData: false, // Important: prevent jQuery from processing the data
                        contentType: false, // Important: let the server handle the content type
                        success: function (data) {
                            Swal.fire({
                                title: '!تم',
                                text: successMessage,
                                icon: 'success',
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'حسنا',
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $('#kt_modal_update').modal('hide');
                                    $('#kt_modal_update').remove();
                                    $(tableSelector).DataTable().ajax.reload(null, false);
                                }
                            });
                        },
                        error: function (xhr, status, error) {
                            // Handle the error here
                            if (xhr.status === 500) {
                                console.error('Resource not found.');
                                window.location.href = '/Error/500.html'; // Replace with the actual URL of your custom 404 page
                            } else {
                                console.error('An error occurred:', error);
                                window.location.href = '/Error/500.html';
                            }
                        },
                    });
                }
            });
        });
    });

}




function mainUpdateHandeller(postUrlAddress, successMessage) {
    $(document).on('click', '.btn-update', function (e) {
        e.preventDefault(); // Prevent default form submission

        const form = document.getElementById('form');
        const formData = new FormData(form);

        var file = formData.get('Image'); // Get the uploaded file

        if (file && file.size !== 0) {
            // Handle the file validation logic (if necessary)

            var validHeaders = [
                [0xFF, 0xD8, 0xFF, 0xE0], // JPEG
                [0x89, 0x50, 0x4E, 0x47], // PNG
                [0xFF, 0xD8, 0xFF] // JPG
            ];

            var fileReader = new FileReader();
            fileReader.onload = function (event) {
                var headerBytes = new Uint8Array(event.target.result).subarray(0, 4);
                var isValidFormat = validHeaders.some(function (validHeader) {
                    return validHeader.every(function (byte, index) {
                        return byte === headerBytes[index];
                    });
                });

                if (!isValidFormat) {
                    Swal.fire({
                        title: 'Error',
                        text: 'Invalid image file format.',
                        icon: 'error',
                        confirmButtonColor: '#d33',
                        confirmButtonText: 'OK'
                    });
                    return; // Stop further execution
                }

                // If image is valid, proceed with the AJAX request
                sendFormData(formData, postUrlAddress, successMessage);
            };

            fileReader.readAsArrayBuffer(file);
        } else {
            // If no image is provided, just submit the form data
            sendFormData(formData, postUrlAddress, successMessage);
        }
    });
}

function sendFormData(formData, postUrlAddress, successMessage) {
    $.ajax({
        url: postUrlAddress,
        type: 'POST',
        cache: false,
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            // Handle the JSON response from the server
            if (response.success) {
                Swal.fire({
                    title: 'تم',
                    text: response.message, // This will be the success message from the server
                    icon: 'success',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'حسنا',
                });
            } else {
                // If the response indicates failure
                Swal.fire({
                    title: 'خطأ',
                    text: response.message, // This will be the failure message from the server
                    icon: 'error',
                    confirmButtonColor: '#d33',
                    confirmButtonText: 'حسنا',
                });
            }
        },
        error: function (xhr, status, error) {
            // Handle AJAX errors (e.g., server error)
            Swal.fire({
                title: 'Error',
                text: 'An error occurred while processing your request.',
                icon: 'error',
                confirmButtonColor: '#d33',
                confirmButtonText: 'حسنا',
            });
        }
    });
}



function showSwalAndSubmitFormWithMultiFiles(formSelector, tableSelector, successMessage, urlAddress) {

    $(formSelector).on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        // Create a new FormData object
        var formData = new FormData(this);

        // Get the uploaded files from the form data
        var files = formData.getAll('Photos');

        // Check each file's extension
        for (var i = 0; i < files.length; i++) {
            var file = files[i];

            if (file) { // Check if the file is not null
                var allowedExtensions = ["jpg", "jpeg", "png"];
                var fileExtension = file.name.split('.').pop().toLowerCase();

                if (allowedExtensions.indexOf(fileExtension) === -1) {
                    // Show error alert using SweetAlert
                    Swal.fire({
                        title: 'Error',
                        text: 'Only JPG, JPEG, and PNG files are allowed.',
                        icon: 'error',
                        confirmButtonColor: '#d33',
                        confirmButtonText: 'OK'
                    });
                    return; // Stop further execution
                }
            }
        }


        $.ajax({
            url: urlAddress, // Replace with your actual server URL to add/update the item
            type: 'POST',
            cache: false,
            data: formData,
            processData: false, // Important: prevent jQuery from processing the data
            contentType: false, // Important: let the server handle the content type
            success: function (data) {
                Swal.fire({
                    title: 'تم!',
                    text: successMessage,
                    icon: 'success',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'OK',
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Redirect to the Index action of the Meal controller
                        //window.location.href = '/Admin/Testimonial/Index';

                        //$('#kt_modal_new_address').modal('hide');
                        //$('#kt_modal_new_address').remove();
                        //$(".modal-backdrop.show").remove();

                        $(tableSelector).DataTable().ajax.reload(null, false);

                        var discardButton = document.querySelector('.btn-discard');
                        if (discardButton) {
                            discardButton.click();
                        }
                    }
                });
            },
            error: function (xhr, status, error) {
                // Handle the error here
                if (xhr.status === 500) {
                    // Handle 400 error
                    console.error('Resource not found.');

                    // Redirect to the custom 404 page
                    window.location.href = '/Error/500.html'; // Replace with the actual URL of your custom 404 page
                } else {
                    // Handle other error codes
                    console.error('An error occurred:', error);
                    window.location.href = '/Error/500.html'
                }
            },
        });

    });


    // Reset the form on Discard button click
    $(formSelector).on('click', '[type="reset"]', function () {
        document.querySelector(formSelector).reset();
    });
}




function updateHandellerWithMultiFiles(tableSelector, postUrlAddress, getUrlAddress, successMessage) {
    $(tableSelector).on('click', '.btn-update', function () {
        // Load the modal content using AJAX
        var Id = $(this).data('id');
        var UrlUpdate = getUrlAddress + Id;

        $.get(UrlUpdate, function (data) {
            $(".modal-backdrop.show").remove();
            $("#ShowModal").html(data);
            $("#kt_modal_update").modal("show");

            // Attach an event listener to the submit button inside the modal
            $("#kt_modal_update").on('submit', 'form', function (e) {
                e.preventDefault(); // Prevent default form submission

                var formData = new FormData(this);
                
                var files = formData.get('Image'); // Get the uploaded file
                /*if (files.length !== 0) {
                    // Define header signatures for valid image formats
                    var validHeaders = [
                        [0xFF, 0xD8, 0xFF, 0xE0], // JPEG
                        [0x89, 0x50, 0x4E, 0x47], // PNG
                        [0xFF, 0xD8, 0xFF] // JPG
                    ];

                    var fileReader = new FileReader();
                    fileReader.onload = function (event) {
                        var headerBytes = new Uint8Array(event.target.result).subarray(0, 4);

                        var isValidFormat = validHeaders.some(function (validHeader) {
                            return validHeader.every(function (byte, index) {
                                return byte === headerBytes[index];
                            });
                        });

                        if (!isValidFormat) {
                            Swal.fire({
                                title: 'Error',
                                text: 'Invalid image file format.',
                                icon: 'error',
                                confirmButtonColor: '#d33',
                                confirmButtonText: 'OK'
                            });
                            return; // Stop further execution
                        }

                        // Continue with AJAX request if image format is valid
                        $.ajax({
                            url: postUrlAddress, // Replace with your actual server URL to add/update the item
                            type: 'POST',
                            cache: false,
                            data: formData,
                            processData: false, // Important: prevent jQuery from processing the data
                            contentType: false, // Important: let the server handle the content type
                            success: function (data) {
                                Swal.fire({
                                    title: '!تم',
                                    text: successMessage,
                                    icon: 'success',
                                    confirmButtonColor: '#3085d6',
                                    confirmButtonText: 'حسنا',
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        $('#kt_modal_update').modal('hide');
                                        $('#kt_modal_update').remove();
                                        $(tableSelector).DataTable().ajax.reload(null, false);
                                    }
                                });
                            },
                            error: function (xhr, status, error) {
                                // Handle the error here
                                if (xhr.status === 500) {
                                    console.error('Resource not found.');
                                    window.location.href = '/Error/500.html'; // Replace with the actual URL of your custom 404 page
                                } else {
                                    console.error('An error occurred:', error);
                                    window.location.href = '/Error/500.html';
                                }
                            },
                        });
                    };

                    // Read the file as an ArrayBuffer
                    fileReader.readAsArrayBuffer(file);
                } else {*/
                    // No image provided, continue with the AJAX request
                    $.ajax({
                        url: postUrlAddress, // Replace with your actual server URL to add/update the item
                        type: 'POST',
                        cache: false,
                        data: formData,
                        processData: false, // Important: prevent jQuery from processing the data
                        contentType: false, // Important: let the server handle the content type
                        success: function (data) {
                            Swal.fire({
                                title: '!تم',
                                text: successMessage,
                                icon: 'success',
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'حسنا',
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $('#kt_modal_update').modal('hide');
                                    $('#kt_modal_update').remove();
                                    $(tableSelector).DataTable().ajax.reload(null, false);
                                }
                            });
                        },
                        error: function (xhr, status, error) {
                            // Handle the error here
                            if (xhr.status === 500) {
                                console.error('Resource not found.');
                                window.location.href = '/Error/500.html'; // Replace with the actual URL of your custom 404 page
                            } else {
                                console.error('An error occurred:', error);
                                window.location.href = '/Error/500.html';
                            }
                        },
                    });
                /*}*/
            });
        });
    });

}




function showSwalAndSubmitFormWithoutFiles(formSelector, tableSelector, successMessage, urlAddress) {
    $(formSelector).on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        // Create a new FormData object
        var formData = new FormData(this);

        // Perform the AJAX request here
        $.ajax({
            url: urlAddress,
            type: 'POST',
            cache: false,
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                Swal.fire({
                    title: '!تم',
                    text: successMessage,
                    icon: 'success',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'حسنا'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Reload data and perform other actions
                        $(tableSelector).DataTable().ajax.reload(null, false);
                        var discardButton = document.querySelector('.btn-discard');
                        if (discardButton) {
                            discardButton.click();
                        }
                    }
                });
            },
            error: function (xhr, status, error) {
                // Handle the error here
                if (xhr.status === 500) {
                    console.error('Resource not found.');
                    console.error(urlAddress);
                    //window.location.href = '/Error/500.html';
                } else {
                    console.error('An error occurred:', error);
                    //window.location.href = '/Error/500.html';
                }
            },
        });
    });

}




function updateHandellerWithoutFiles(tableSelector, postUrlAddress, getUrlAddress, successMessage) {
    $(tableSelector).on('click', '.btn-update', function () {
        // Load the modal content using AJAX
        var Id = $(this).data('id');
        var UrlUpdate = getUrlAddress + Id;

        $.get(UrlUpdate, function (data) {
            $(".modal-backdrop.show").remove();
            $("#ShowModal").html(data);
            $("#kt_modal_update").modal("show");

            // Attach an event listener to the submit button inside the modal
            $("#kt_modal_update").on('submit', 'form', function (e) {
                e.preventDefault(); // Prevent default form submission

                var formData = new FormData(this);

                $.ajax({
                    url: postUrlAddress, // Replace with your actual server URL to add/update the item
                    type: 'POST',
                    cache: false,
                    data: formData,
                    processData: false, // Important: prevent jQuery from processing the data
                    contentType: false, // Important: let the server handle the content type
                    success: function (data) {
                        Swal.fire({
                            title: '!تم',
                            text: successMessage,
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'حسنا',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $('#kt_modal_update').modal('hide');
                                $('#kt_modal_update').remove();
                                $(tableSelector).DataTable().ajax.reload(null, false);
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        // Handle the error here
                        if (xhr.status === 500) {
                            console.error('Resource not found.');
                            window.location.href = '/Error/500.html'; // Replace with the actual URL of your custom 404 page
                        } else {
                            console.error('An error occurred:', error);
                            window.location.href = '/Error/500.html';
                        }
                    },
                });
                    
            });
        });
    });

}


