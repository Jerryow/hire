const gulp = require('gulp'),
    util = require("gulp-util"),
    sass = require("gulp-sass"),
    sourcemaps = require('gulp-sourcemaps'),
    autoprefixer = require('gulp-autoprefixer'),
    minifycss = require('gulp-minify-css'),
    del = require('del'),
    rename = require('gulp-rename'),
    babel = require("gulp-babel"),
    uglify = require('gulp-uglify');
    

//调用js文件合并插件
var concat = require('gulp-concat');

const log = util.log;
const spawn = require('child_process').spawn


gulp.task("sass", function () {
    return gulp.src(['Assets/scss/**/*.scss'])
        .pipe(sourcemaps.init())
        .pipe(sass({ style: 'expanded', outputStyle: 'compressed' }))
        .pipe(sourcemaps.write())
        .pipe(autoprefixer("last 3 version", "safari 5", "ie 8", "ie 9"))
        .pipe(minifycss())
        .pipe(gulp.dest("Assets/css"))
    //.pipe(rename({ suffix: '.min' }))
    //.pipe(minifycss())
    //.pipe(gulp.dest('Assets/css/min'));
});

gulp.task("js", function () {
    return gulp.src(['Js/**/*.js', '!Js/profile/**'])
        .pipe(babel())
        .pipe(gulp.dest('Assets/js'))
});

gulp.task("js:min", function () {
    return gulp.src(['Js/**/*.js','!Js/profile/**'])
        .pipe(babel())
        .pipe(uglify())
        .pipe(gulp.dest('Assets/js'))
});

gulp.task("js:concat", function () {
    return gulp.src(['Js/profile/*.js'])
        .pipe(concat('index.js'))
        .pipe(babel({ presets: ['es2015'] }))
        .pipe(gulp.dest('Assets/js/resume'))
});
gulp.task("js:concat:min", function () {
    return gulp.src(['Js/profile/*.js'])
        .pipe(concat('index.js'))
        .pipe(babel({ presets: ['es2015'] }))
        .pipe(uglify())
        .pipe(gulp.dest('Assets/js/resume'))
});

gulp.task('watch', function () {//监听变化执行任务
    //当匹配任务变化才执行相应任务
    gulp.watch('Assets/scss/**/*.scss', gulp.series('sass'));
    gulp.watch(['Js/**/*.js', '!Js/profile/**'], gulp.series('js'));
    gulp.watch('Js/profile/*.js', gulp.series('js:concat'));
})

gulp.task('clean', function () {
    return del([
        'Assets/css/**/*',
        'Assets/js/**/*'
    ]);
});

gulp.task('css:clean', function () {
    return del([
        'Assets/css/**/*'
    ]);
});

gulp.task('js:clean', function () {
    return del([
        'Assets/js/**/*'
    ]);
});

//gulp.series|4.0 依赖
//gulp.parallel|4.0 多个依赖嵌套
gulp.task('build:min', gulp.series(gulp.parallel('sass', 'js:min', 'js:concat:min')));
gulp.task('build', gulp.series(gulp.parallel('sass', 'js', 'js:concat')));

//exports.default = convertSass
