import {
  bindable,
  autoinject,
} from "aurelia-framework";

@autoinject
export class headerCustomElement {
  @bindable title: string;
  constructor() {}
}
