import { createReducer, on } from "@ngrx/store";
import { ListState } from "../states/list.state";
import { loadListFail, loadListSuccess } from "../actions/list.action";


const _ListReducer = createReducer(ListState,
    on(loadListSuccess, (state, action) => {
        return{
            ...state,
            lists: [...action.lists],
            errorMassege: ''
        }
    }),
    on(loadListFail, (state, action) => {
        return{
            ...state,
            lists: [],
            errorMassege: action.errorMassege
        }
    })
)

export function ListReducer(state: any, action: any){
    return _ListReducer(state,action);
}