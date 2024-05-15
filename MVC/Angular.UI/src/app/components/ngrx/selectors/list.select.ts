import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ListModel } from "../../../models/list";

const getListState = createFeatureSelector<ListModel>('list');

export const getlists = createSelector(getListState,(state)=>{
    return state.lists;
})