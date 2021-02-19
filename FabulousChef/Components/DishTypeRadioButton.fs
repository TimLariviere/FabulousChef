module FabulousChef.Components.DishTypeRadioButton

open FabulousChef

open Fabulous.XamarinForms
open Xamarin.Forms
open Xamarin.Forms.Internals

let dishTypeRadioButtonControlTemplate =
    ControlTemplate(fun () ->
        // content
        let frame = Frame()
        frame.Padding <- Thickness(0.)
        frame.HeightRequest <- 150.
        frame.WidthRequest <- 50.
        frame.HasShadow <- false
        frame.CornerRadius <- 30.f
        
        let grid = Grid()
        
        let label = Label()
        label.HeightRequest <- 50.
        label.WidthRequest <- 150.
        label.HorizontalTextAlignment <- TextAlignment.Center
        label.VerticalTextAlignment <- TextAlignment.Center
        label.Rotation <- -90.
        label.LineBreakMode <- LineBreakMode.WordWrap
        label.SetBinding(Label.TextProperty, Binding("Content", source = RelativeBindingSource.TemplatedParent))
        grid.Children.Add(label)
        
        frame.Content <- grid
        
        NameScope.SetNameScope(frame, NameScope())
        (frame :> INameScope).RegisterName("label", label)
        
        // Visual States
        let visualStateGroupList = VisualStateGroupList()
        VisualStateManager.SetVisualStateGroups(frame, visualStateGroupList)
        
        let checkedStates = VisualStateGroup()
        checkedStates.Name <- "CheckedStates"
        visualStateGroupList.Add(checkedStates)
        
        let checkedState = VisualState()
        checkedState.Name <- "Checked"
        checkedStates.States.Add(checkedState)
        
        let checkedStateBackgroundColorSetter = Setter()
        checkedStateBackgroundColorSetter.Property <- Frame.BackgroundColorProperty
        checkedStateBackgroundColorSetter.Value <- Colors.selectedDishTypeBackground ()
        checkedState.Setters.Add(checkedStateBackgroundColorSetter)
        
        let checkedStateTextColorSetter = Setter()
        checkedStateTextColorSetter.TargetName <- "label"
        checkedStateTextColorSetter.Property <- Label.TextColorProperty
        checkedStateTextColorSetter.Value <- Colors.selectedDishTypeForeground ()
        checkedState.Setters.Add(checkedStateTextColorSetter)
        
        let uncheckedState = VisualState()
        uncheckedState.Name <- "Unchecked"
        checkedStates.States.Add(uncheckedState)
        
        let uncheckedStateBackgroundColorSetter = Setter()
        uncheckedStateBackgroundColorSetter.Property <- Frame.BackgroundColorProperty
        uncheckedStateBackgroundColorSetter.Value <- Colors.unselectedDishTypeBackground ()
        uncheckedState.Setters.Add(uncheckedStateBackgroundColorSetter)
        
        let uncheckedStateTextColorSetter = Setter()
        uncheckedStateTextColorSetter.TargetName <- "label"
        uncheckedStateTextColorSetter.Property <- Label.TextColorProperty
        uncheckedStateTextColorSetter.Value <- Colors.unselectedDishTypeForeground ()
        uncheckedState.Setters.Add(uncheckedStateTextColorSetter)
        
        frame :> obj
    )

type Fabulous.XamarinForms.View with
    static member inline DishTypeRadioButton(?groupName, ?isChecked, ?checkedChanged, ?content) =
        View.RadioButton(
            controlTemplate = dishTypeRadioButtonControlTemplate,
            ?groupName = groupName,
            ?isChecked = isChecked,
            ?checkedChanged = checkedChanged,
            ?content = content
        )