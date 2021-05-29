module Components.StoreProvider

open Feliz
open Prelude.Redux
open StateStore.Main

let private ctx =
    React.createContext<StateStore<AppState>> ("stateStore", stateStore())

[<ReactComponent>]
let StoreProvider (store: StateStore<AppState>) (child: ReactElement) =
    React.contextProvider (ctx, store, child)

let useSelector (transform: AppState -> 'a) =
    let store = React.useContext ctx
    let state = store.GetState()
    let transformed _ = transform state

    let (selectedState, setSelectedState) = React.useState (transformed)
    store.Subscribe(transform, setSelectedState)

    selectedState

let useDispatch () : obj -> unit =
    React.useContext(ctx).Dispatch
