module FabulousChef.Controls.LikeButton

open Fabulous.XamarinForms
open Xamarin.Forms
open FabulousChef
    
type Fabulous.XamarinForms.View with
    static member inline LikeButton(?margin, ?height, ?width, ?horizontalOptions, ?verticalOptions) =
        View.ImageButton(
            ?margin = margin,
            ?height = height,
            ?width = width,
            ?horizontalOptions = horizontalOptions,
            ?verticalOptions = verticalOptions,
            source = Image.fromFont(
                 FontImageSource(
                     FontFamily = Fonts.Icomoon,
                     Glyph = Fonts.IcomoonConstants.Heart,
                     Color = Colors.uncheckedHeartColor ()
                 )
            )
        )
        