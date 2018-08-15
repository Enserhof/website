var path = require("path");
var webpack = require("webpack");
var fableUtils = require("fable-utils");
const workboxPlugin = require('workbox-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const SitemapPlugin = require('sitemap-webpack-plugin').default;

function resolve(filePath) {
    return path.join(__dirname, filePath)
}

var babelOptions = fableUtils.resolveBabelOptions({
    presets: [["env", { "modules": false }]],
    plugins: [["transform-runtime", {
        "helpers": true,
        "polyfill": true,
        "regenerator": false
    }]]
});

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
    mode: isProduction ? "production" : "development",
    devtool: isProduction ? undefined : "source-map",
    entry: {
        client: resolve('./src/Client/Client.fsproj')
    },
    output: {
        path: resolve('./public'),
        filename: "[name].js",
        globalObject: "this" //https://github.com/webpack/webpack/issues/6642
    },
    resolve: {
        modules: [
            "node_modules", resolve("./node_modules/")
        ]
    },
    devServer: {
        contentBase: resolve('./public'),
        port: 8080,
        hot: true,
        inline: true
    },
    module: {
        rules: [
            {
                test: /\.fs(x|proj)?$/,
                use: {
                    loader: "fable-loader",
                    options: {
                        babel: babelOptions,
                        define: isProduction ? [] : ["DEBUG"]
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: babelOptions
                },
            },
            {
                test: /\.(sass|scss|css)$/,
                use: [
                    "style-loader",
                    "css-loader",
                    "sass-loader"
                ]
            },
            {
                test: /\.(png|jpg|gif)$/,
                use: "file-loader"
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
                use: "file-loader"
            }
        ]
    },
    plugins: [
        ...(isProduction ? [] : [new webpack.HotModuleReplacementPlugin()]),
        ...(isProduction ? [] : [new webpack.NamedModulesPlugin()]),
        new HtmlWebpackPlugin({
            template: 'src/index.html'
        }),
        new CopyWebpackPlugin([
            "src/404.html",
            "src/manifest.json",
            { from: "**", to: "icons/", context: "src/icons" },
            { from: "src/Server/", to: "api/" }
        ]),
        new SitemapPlugin('https://enserhof.github.io', [
            "/aktivitaeten",
            "/ueber-den-hof/expand-all",
            "/lageplan"
        ]),
        new workboxPlugin.GenerateSW({
            swDest: "sw.js",

            exclude: [
                /\.(?:png|jpg|jpeg|svg)$/,
                /^manifest\.json$/,
                /^api\//
            ],

            runtimeCaching: [
                {
                    urlPattern: /\.(?:png|jpg|jpeg|svg)$/,
                    handler: 'cacheFirst',
                    options: {
                        cacheName: 'images',
                        expiration: {
                            maxAgeSeconds: 60 * 60 * 24 * 7 // 1 week
                        },
                    },
                },
                {
                    urlPattern: /\/api\/(?:.*)/,
                    handler: 'networkFirst',
                    options: {
                        cacheName: 'api-cache'
                    },
                },
                {
                    urlPattern: new RegExp('https://fonts.(?:googleapis|gstatic).com/(.*)'),
                    handler: 'cacheFirst',
                    options: {
                        cacheName: 'google-fonts',
                        cacheableResponse: {
                            statuses: [0, 200]
                        },
                        expiration: {
                            maxEntries: 30
                        },
                    },
                }
            ]
        })
    ]
};
