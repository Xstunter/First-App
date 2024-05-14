import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { HttpListService } from "../../../services/list.service";
import { loadList, loadListFail, loadListSuccess } from "../actions/list.action";
import { catchError, exhaustMap, of, map } from "rxjs";

@Injectable()
export class ListEffects {
    constructor(private action$: Actions, private service: HttpListService) {}

    loadCard$ = createEffect(() =>
        this.action$.pipe(
            ofType(loadList),
            exhaustMap((action)=>{
                return this.service.getAllLists(action.id).pipe(
                    map((data)=>{
                        return loadListSuccess({lists:data})
                    }),
                    catchError((_error)=>of(loadListFail({errorMassege:_error.massege})))
                )
            })
        )
    );
}