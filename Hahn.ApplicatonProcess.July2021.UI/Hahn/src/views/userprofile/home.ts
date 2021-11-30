import { Router } from "aurelia-router";
import { LocalizationService } from './../../resources/services/localizationService';
import {
  inject,
  CompositionTransaction,
  CompositionTransactionNotifier,
  observable
} from "aurelia-framework";

@inject(
  Router,
  LocalizationService
)
export class Home {
  private localizationService: LocalizationService;
  private router: Router;
  private showSuccessMsg = false;
  private deleteSuccessMsg: string;
  private titleCreatePage: string;

  constructor(router: Router,
    localizationService: LocalizationService) {
    this.router = router;
    this.localizationService = localizationService;
    this.titleCreatePage = this.localizationService.titleCreatePage;
    this.deleteSuccessMsg = this.localizationService.deleteSuccessMsg;  
  }

  activate = (params) => {
    if (params.action) {
      this.showSuccessMsg = true;
    }
  }

  GoToNewPage() {
    this.router.navigateToRoute("createProfile");
  }

}
