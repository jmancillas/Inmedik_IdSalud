﻿/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({        
        paths: {
            // paths serve as alias
            'npm:': '/libs/'
        },
        // map tells the System loader where to look for things
        map: {
            // our app is within the app folder
            app: '/Scripts',
            // angular bundles
            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
            // other libraries
            'rxjs': 'npm:rxjs',
            'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/bundles/in-memory-web-api.umd.js',
            'angular-datatables': 'node_modules/angular-datatables',
            'plugin-babel': 'node_modules/systemjs-plugin-babel/plugin-babel.js',
            'systemjs-babel-build': 'node_modules/systemjs-plugin-babel/systemjs-babel-browser.js',
            'ngx-bootstrap': 'node_modules/ngx-bootstrap',
            //'ngx-bootstrap/modal': 'node_modules/ngx-bootstrap/modal'
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            app: {
                main: './main.js',
                defaultExtension: 'js',
            },
            rxjs: {
                defaultExtension: 'js'
            },
            "angular-datatables": {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ngx-bootstrap': { format: 'cjs', main: 'bundles/ngx-bootstrap.umd.js', defaultExtension: 'js' },
            //"ngx-bootstrap/modal": {
            //    main: 'index.js',
            //    defaultExtension: 'js'
            //}
        },
        transpiler: 'plugin-babel'
    });
})(this);