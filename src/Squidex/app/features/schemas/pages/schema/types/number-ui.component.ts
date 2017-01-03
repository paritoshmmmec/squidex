/*
 * Squidex Headless CMS
 * 
 * @license
 * Copyright (c) Sebastian Stehle. All rights reserved
 */

import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';

import {
    fadeAnimation,
    FloatConverter,
    NumberFieldPropertiesDto
} from 'shared';

@Component({
    selector: 'sqx-number-ui',
    styleUrls: ['number-ui.component.scss'],
    templateUrl: 'number-ui.component.html',
    animations: [
        fadeAnimation
    ]
})
export class NumberUIComponent implements OnDestroy, OnInit {
    private editorSubscription: Subscription;

    @Input()
    public editForm: FormGroup;

    @Input()
    public properties: NumberFieldPropertiesDto;

    public converter = new FloatConverter();

    public hideAllowedValues: Observable<boolean>;

    public ngOnInit() {
        this.editForm.addControl('editor',
            new FormControl(this.properties.editor, [
                Validators.required
            ]));
        this.editForm.addControl('placeholder',
            new FormControl(this.properties.placeholder, [
                Validators.maxLength(100)
            ]));
        this.editForm.addControl('allowedValues',
            new FormControl(this.properties.allowedValues, []));

        this.hideAllowedValues =
            Observable.of(this.properties.editor)
                .merge(this.editForm.get('editor').valueChanges)
                .map(x => !x || x === 'Input' || x === 'Textarea');

        this.editorSubscription =
            this.hideAllowedValues.subscribe(isSelection => {
                if (isSelection) {
                    this.editForm.get('allowedValues').setValue(undefined);
                }
            });
    }

    public ngOnDestroy() {
        this.editorSubscription.unsubscribe();
    }
}