module FabulousChef.Controls.ReviewsSummary

open FabulousChef

open Fabulous.XamarinForms
open Xamarin.Forms

type RatingComponentData =
    { AverageReviews: float
      ReviewsCount: int }
    
type Fabulous.XamarinForms.View with
    static member inline ReviewsSummary(?averageReviews: float, ?reviewsCount: int, ?horizontalOptions) =
        let averageReviews = match averageReviews with Some x -> x | None -> 5.
        let reviewsCount = match reviewsCount with Some x -> x | None -> 0
        
        View.StackLayout(
            ?horizontalOptions = horizontalOptions,
            orientation = StackOrientation.Horizontal,
            children = [
                if averageReviews <= 5. then
                    Fonts.IcomoonConstants.ok (FontSize.fromValue 20.)
                else
                    Fonts.IcomoonConstants.fire (FontSize.fromValue 20.)
                    
                View.Label(
                    formattedText = View.FormattedString(
                        spans = [
                            View.Span(
                                text = sprintf "%.2f" averageReviews,
                                fontFamily = Fonts.OpenSansSemibold,
                                textColor = Colors.averageReviewTextForeground ()
                            )
                            View.Span(
                                text = sprintf " (%i)" reviewsCount,
                                fontFamily = Fonts.OpenSansLight,
                                textColor = Colors.reviewCountTextForeground ()
                            )
                        ]
                    ),
                    fontSize = FontSize.fromValue 16.
                )
            ]
        )
       