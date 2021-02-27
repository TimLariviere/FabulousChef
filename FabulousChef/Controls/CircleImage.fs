module FabulousChef.Controls.CircleImage

open Fabulous.XamarinForms
open Xamarin.Forms

type Fabulous.XamarinForms.View with
    static member inline CircleImage(?source, ?height, ?width, ?horizontalOptions, ?verticalOptions) =
        let height = match height with Some x -> x | None -> 0.
        let width = match width with Some x -> x | None -> 0.
        let centerX = width / 2.
        let centerY = height / 2.
        let radiusX = width / 2.
        let radiusY = height / 2.
        
        View.Image(
            ?source = source,
            ?horizontalOptions = horizontalOptions,
            ?verticalOptions = verticalOptions,
            height = height,
            width = width,
            aspect = Aspect.AspectFill,
            clip = View.EllipseGeometry(
                center = Point(centerX, centerY),
                radiusX = radiusX,
                radiusY = radiusY
            )
        )

