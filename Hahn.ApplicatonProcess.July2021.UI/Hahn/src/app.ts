import { LocalizationService } from './resources/services/localizationService';
import { Router, RouterConfiguration } from "aurelia-router";
import { inject, PLATFORM } from "aurelia-framework";
import { I18N } from "aurelia-i18n";


@inject(
  I18N,
  LocalizationService
)
export class App {
  public appTitle;
  router: Router | undefined;
  private i18N: I18N;
  private localizationService: LocalizationService;

  constructor(i18N: I18N, localizationService: LocalizationService) {
    this.i18N = i18N;
    this.i18N.setLocale('de');
    this.localizationService = localizationService;
    this.appTitle = this.localizationService.appTitle;
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "Hahn Applicant Process";
    config.options.pushState = true;
    //config.options.root = "/";
    config.map([
      {
        route: [""],
        name: "createProfile",
        moduleId: PLATFORM.moduleName("views/userprofile/createUserProfile"),
        title: this.localizationService.titleCreatePage,
        nav: true,
      },
      {
        route: ["view-profile"],
        name: "viewProfile",
        moduleId: PLATFORM.moduleName("views/userprofile/viewUserProfile"),
        title: this.localizationService.titleViewPage,
        
      },
      {
        route: ["edit-profile/:id"],
        name: "editProfile",
        moduleId: PLATFORM.moduleName("views/userprofile/createUserProfile"),
        title: this.localizationService.titleUpdatePage
      }
    ]);

    this.router = router;
  };

}
