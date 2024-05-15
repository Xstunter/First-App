import { createAction, props } from "@ngrx/store";
import { IList } from "../../../models/list";

export const LOAD_List = '[list page]load list'
export const LOAD_List_SUCCESS = '[list page]load list success'
export const LOAD_List_FAIL = '[list page]load list fail'

export const loadList = createAction(LOAD_List, props<{ id: number }>());
export const loadListSuccess = createAction(LOAD_List_SUCCESS, props<{ lists : IList[] }>());
export const loadListFail = createAction(LOAD_List_FAIL, props<{ errorMassege: string }>());