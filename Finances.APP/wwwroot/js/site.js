// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Debug script for Bootstrap 5 dropdowns
$(document).ready(function() {
    console.log('Site.js loaded');
    
    // Check if Bootstrap 5 is available
    if (typeof bootstrap !== 'undefined') {
        console.log('Bootstrap 5 is loaded');
        
        // Initialize collapse components
        var collapseElementList = [].slice.call(document.querySelectorAll('.sidebar .collapse'));
        console.log('Found collapse elements:', collapseElementList.length);
        
        // Add event listeners to debug
        $('.sidebar .nav-link[data-bs-toggle="collapse"]').on('click', function(e) {
            console.log('Dropdown clicked:', $(this).attr('data-bs-target'));
        });
        
        // Listen for collapse events
        $('.sidebar .collapse').on('show.bs.collapse', function() {
            console.log('Collapse showing:', this.id);
        });
        
        $('.sidebar .collapse').on('hide.bs.collapse', function() {
            console.log('Collapse hiding:', this.id);
        });
        
    } else {
        console.log('Bootstrap 5 not found');
    }
});
