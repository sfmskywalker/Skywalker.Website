const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CopyPlugin = require('copy-webpack-plugin');
const path = require('path');

module.exports = {

    entry: './index.js',

    output: {
        path: path.resolve(__dirname, 'wwwroot'),
        filename: 'bundle.js'
    },

    mode: 'development',

    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            },
            {
                test: /\.(sa|sc|c)ss$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        loader: "css-loader",
                    },
                    {
                        loader: "postcss-loader"
                    },
                    {
                        loader: "sass-loader",
                        options: {
                            implementation: require("sass")
                        }
                    }
                ]
            },
            {
                test: /\.(png|jpe?g|gif|svg)$/,
                use: [
                    {
                        loader: "file-loader",

                        // In options we can set different things like format
                        // and directory to save
                        options: {
                            outputPath: 'images'
                        }
                    }
                ]
            }
        ]
    },

    plugins: [

        new MiniCssExtractPlugin({
            filename: "bundle.css"
        }),
        new CopyPlugin([
            {from: 'Assets', to: './'}
        ])
    ]
};