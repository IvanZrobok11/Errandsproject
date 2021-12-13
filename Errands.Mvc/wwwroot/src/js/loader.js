document.body.onload = function() {

    setTimeout(function() {
        var loader = document.getElementById('loader'); 
        console.log('page loaded');
        var wrapper = document.getElementById('wrapper');
        if( !loader.classList.contains('loader__done') && !wrapper.classList.contains('wrapper__done'))
        {
            wrapper.classList.add('wrapper__done')
            loader.classList.add('loader__done');
        }
    },150)
}