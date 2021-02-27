module FabulousChef.Pages.DishPage

open FabulousChef
open FabulousChef.Models
open Fabulous.XamarinForms
open Xamarin.Forms

open FabulousChef.Controls.CircleImage
open FabulousChef.Controls.LikeButton
open FabulousChef.Controls.PreparationTimeLabel
open FabulousChef.Controls.ReviewsSummary
open FabulousChef.PancakeViewExtensions

type Model =
    { Id: DishId
      Dish: Dish
      Chef: Chef
      AverageReviews: float }
    
let init (id: DishId) =
    let dish = Data.getDishById id
    let chef = Data.getChefForDishId id
    let averageReviews =
        if dish.Reviews.Length = 0 then
            5.
        else
            dish.Reviews
            |> List.map float
            |> List.average
            
    { Id = id
      Dish = dish
      Chef = chef
      AverageReviews = averageReviews }
    
let update (msg: unit) model =
    model
    
let headerView model =
    View.StackLayout(
        spacing = 15.,
        children = [
            View.Label(
                horizontalOptions = LayoutOptions.Center,
                text = model.Dish.Name,
                fontSize = FontSize.fromValue 22.,
                fontFamily = Fonts.OpenSansBold
            )
            
            View.ReviewsSummary(
                averageReviews = model.AverageReviews,
                reviewsCount = model.Dish.Reviews.Length,
                horizontalOptions = LayoutOptions.Center
            )
            
            View.Grid([
                View.Image(
                    source = Image.fromPath model.Dish.Picture,
                    margin = Thickness(0., 0., 0., 30.),
                    height = 280.
                )
                View.CircleImage(
                    source = Image.fromPath model.Chef.Picture,
                    horizontalOptions = LayoutOptions.Center,
                    verticalOptions = LayoutOptions.End,
                    height = 80.,
                    width = 80.
                )
                View.LikeButton(
                    height = 30.,
                    width = 30.,
                    horizontalOptions = LayoutOptions.End,
                    verticalOptions = LayoutOptions.End,
                    margin = Thickness(0., 0., 20., 0.)
                )
            ])
        ]
    )
    
let recipeView recipe =
    View.PancakeView(
        cornerRadius = CornerRadius(10.),
        backgroundColor = Colors.controlBackground (),
        padding = Thickness(15.),
        margin = Thickness(5., 10., 5., 0.),
        content = View.StackLayout([
            View.StackLayout(
                orientation = StackOrientation.Horizontal,
                children = [
                    View.Label(
                        text = "Recipe",
                        fontSize = FontSize.fromValue 24.,
                        horizontalOptions = LayoutOptions.StartAndExpand,
                        verticalOptions = LayoutOptions.Center
                    )
                    View.PreparationTimeLabel(
                        preparationTime = recipe.PreparationTime
                    )
                ]
            )
            
            if recipe.Steps.Length = 0 then
                View.Label(
                    text = "No recipe step",
                    textColor = Colors.textForeground ()
                )
            else
                View.StackLayout(
                    spacing = 15.,
                    children = [
                        for i = 0 to recipe.Steps.Length - 1 do
                            View.StackLayout(
                                children = [
                                    View.Label(
                                        text = $"Step {i + 1}",
                                        textColor = Colors.textForeground ()
                                    )
                                    View.Label(
                                        text = recipe.Steps.[i],
                                        textColor = Colors.reviewCountTextForeground ()
                                    )
                                ]
                            )
                    ]
                )
        ])
    )
    
let view model dispatch =
    View.ContentPage(
        useSafeArea = false,
        backgroundColor = Colors.appBackground (),
        content = View.ScrollView(
            View.StackLayout([
                headerView model
                recipeView model.Dish.Recipe
            ])
        )
    ).BackButtonTitle("")
    
let program =
    XamarinFormsProgram.mkSimple init update view
    
type Fabulous.XamarinForms.View with
    static member inline DishPage(dishId) =
        Component.forProgramWithArgs(program, dishId)