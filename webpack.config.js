// Template for webpack.config.js in Fable projects
// Find latest version in https://github.com/fable-compiler/webpack-config-template

// In most cases, you'll only need to edit the CONFIG object (after dependencies)
// See below if you need better fine-tuning of Webpack options

// Dependencies. Also required: sass, sass-loader, css-loader, style-loader, file-loader, resolve-url-loader
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const GoogleFontsPlugin = require('@beyonk/google-fonts-webpack-plugin');
const workboxPlugin = require('workbox-webpack-plugin');
const SitemapPlugin = require('sitemap-webpack-plugin').default;

var CONFIG = {
    // The tags to include the generated JS and CSS will be automatically injected in the HTML template
    // See https://github.com/jantimon/html-webpack-plugin
    indexHtmlTemplate: './src/index.html',
    fsharpEntry: './src/Client/App.fs.js',
    cssEntry: './sass/main.sass',
    outputDir: './deploy',
    assetsDir: './public',
    publicPath: '/', // Where the bundled files are accessible relative to server root
    devServerPort: 8080,
    // When using webpack-dev-server, you may need to redirect some calls
    // to a external API server. See https://webpack.js.org/configuration/dev-server/#devserver-proxy
    devServerProxy: undefined,
}

// If we're running webpack serve, assume we're in development
var isProduction = !hasArg(/serve/);
var outputWebpackStatsAsJson = hasArg('--json');

if (!outputWebpackStatsAsJson) {
    console.log('Bundling CLIENT for ' + (isProduction ? 'production' : 'development') + '...');
}
// The HtmlWebpackPlugin allows us to use a template for the index.html page
// and automatically injects <script> or <link> tags for generated bundles.
var commonPlugins = [
    new HtmlWebpackPlugin({
        filename: 'index.html',
        template: resolve(CONFIG.indexHtmlTemplate)
    }),
    new GoogleFontsPlugin({
        fonts: [
            { family: 'Architects Daughter', variants: ['400'] }
        ],
        apiUrl: 'https://gwfh.mranftl.com/api/fonts'
    })
];

module.exports = {
    // In development, bundle styles together with the code so they can also
    // trigger hot reloads. In production, put them in a separate CSS file.
    entry: isProduction ?
        { app: [resolve(CONFIG.fsharpEntry), resolve(CONFIG.cssEntry)] } :
        {
            app: [resolve(CONFIG.fsharpEntry)],
            style: [resolve(CONFIG.cssEntry)]
        },
    // Add a hash to the output file name in production
    // to prevent browser caching if code changes
    output: {
        publicPath: CONFIG.publicPath,
        path: resolve(CONFIG.outputDir),
        filename: isProduction ? '[name].[contenthash].js' : '[name].js'
    },
    mode: isProduction ? 'production' : 'development',
    devtool: isProduction ? 'source-map' : 'eval-source-map',
    optimization: {
        splitChunks: {
            chunks: 'all'
        },
        moduleIds: isProduction ? 'deterministic' : 'named'
    },
    resolve: {
        // See https://github.com/fable-compiler/Fable/issues/1490
        symlinks: false
    },
    devServer: {
        // Necessary when using non-hash client-side routing
        // This assumes the index.html is accessible from server root
        // For more info, see https://webpack.js.org/configuration/dev-server/#devserverhistoryapifallback
        historyApiFallback: {
            index: '/'
        },
        static: [
            {
              directory: resolve(CONFIG.assetsDir),
              publicPath: '/',
            },
            {
              directory: resolve('./src/Server'),
              publicPath: '/',
            },
        ],
        port: CONFIG.devServerPort,
        proxy: CONFIG.devServerProxy,
        hot: true,
    },
    // - sass-loaders: transforms SASS/SCSS into JS
    // - file-loader: Moves files referenced in the code (fonts, images) into output folder
    module: {
        rules: [
            {
                test: /\.(sass|scss|css)$/,
                use: [
                    isProduction
                        ? MiniCssExtractPlugin.loader
                        : 'style-loader',
                    'css-loader',
                    'resolve-url-loader',
                    {
                        loader: 'sass-loader',
                        options: { implementation: require('sass') }
                    }
                ],
            },
            {
                test: /\.(png|jpg|jpeg|gif|svg|woff|woff2|ttf|eot)(\?.*)?$/,
                type: 'asset/resource'
            }
        ]
    },
    plugins: isProduction ?
        commonPlugins.concat([
            new MiniCssExtractPlugin({ filename: 'style.css' }),
            new CopyWebpackPlugin({patterns: [{ from: resolve(CONFIG.assetsDir) }]}),
            new SitemapPlugin({base: 'https://enserhof.at', paths: [
                '/',
                '/angebote',
                '/ueber-den-hof?expand-all=1',
                '/lageplan'
            ]}),
            new workboxPlugin.GenerateSW({
                swDest: 'sw.js',

                exclude: [
                    /\.(?:png|jpg|jpeg|svg)$/,
                    /^manifest\.json$/,
                    /^api\b/
                ],

                runtimeCaching: [
                    {
                        urlPattern: /\.(?:png|jpg|jpeg|svg)$/,
                        handler: 'CacheFirst',
                        options: {
                            cacheName: 'images',
                            expiration: {
                                maxAgeSeconds: 60 * 60 * 24 * 7 // 1 week
                            },
                        },
                    },
                    {
                        urlPattern: /\/api\/(?:.*)/,
                        handler: 'NetworkFirst',
                        options: {
                            cacheName: 'api-cache'
                        },
                    },
                    {
                        urlPattern: new RegExp('https://fonts.(?:googleapis|gstatic).com/(.*)'),
                        handler: 'CacheFirst',
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
        ]) :
        commonPlugins
};

function resolve(filePath) {
    return path.isAbsolute(filePath) ? filePath : path.join(__dirname, filePath);
}

function hasArg(arg) {
    return arg instanceof RegExp
        ? process.argv.some(x => arg.test(x))
        : process.argv.indexOf(arg) !== -1;
}
