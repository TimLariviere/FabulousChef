module Fabulous.XamarinForms.Component
    open Fabulous
    open Fabulous.XamarinForms.Component
    
    let forProgramWithArgsAndState(program, state: 'state, onStateChanged) =
        let handler = ComponentHandler<'state, 'msg, 'model, 'externalMsg>()
        ComponentViewElement(handler, program, ValueNone, state, ValueSome (onStateChanged, state), ValueNone)
        
    
    let forProgramWithArgsAndExternalMsg(program, args, onExternalMsg) =
        let handler = ComponentHandler<'args, 'msg, 'model, 'externalMsg>()
        ComponentViewElement(handler, program, ValueNone, args, ValueNone, ValueSome onExternalMsg)

