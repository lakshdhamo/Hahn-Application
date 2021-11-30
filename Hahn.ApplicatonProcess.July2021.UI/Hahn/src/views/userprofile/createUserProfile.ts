import { LocalizationService } from './../../resources/services/localizationService';
import { BootstrapFormRenderer } from '../../resources/Validation/bootstrap-form-renderer';
import {
  inject,
  CompositionTransaction,
  CompositionTransactionNotifier,
  observable
} from "aurelia-framework";
import { Router } from "aurelia-router";
import { EventAggregator } from "aurelia-event-aggregator";
import { UserService } from "../../resources/services/userService";
import { User } from "../../resources/models/user";
import { DialogService } from "aurelia-dialog";
import { Dialog } from "../../resources/dialog/dialog";
import { I18N } from "aurelia-i18n";
import {
  ValidationControllerFactory,
  ValidationRules,
} from "aurelia-validation";

@inject(
  UserService,
  ValidationControllerFactory,
  CompositionTransaction,
  Router,
  I18N,
  DialogService,
  EventAggregator,
  LocalizationService
)
export class CreateUserProfile {
  private userService: UserService;
  private controllerFactory: ValidationControllerFactory;
  private notifier: CompositionTransactionNotifier;
  private router: Router;
  private i18N: I18N;
  private dialogService: DialogService;
  private ea: EventAggregator;
  private localizationService: LocalizationService;
  controller = null;
  private title: string;
  validation: any;
  standardGetMessage: any;
  user: User;
  assets: [];
  @observable query;
  selectedAssets = [];
  private isCreatePage = true;
  private submitBtnCaption: string;
  private resetBtnCaption: string;
  
  private enterFirstName: string;
  private enterLastName: string;
  private enterAge: string;
  private enterAddress: string;
  private enterEmail: string;
  private enterAsset: string;

  constructor(
    userService: UserService,
    controllerFactory: { createForCurrentScope: () => any; },
    compositionTransaction: { enlist: () => CompositionTransactionNotifier; },
    router: Router,
    i18N: I18N,
    dialogService: DialogService,
    ea: EventAggregator,
    localizationService: LocalizationService
  ) {
    this.userService = userService;
    this.controller = controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.notifier = compositionTransaction.enlist();
    this.router = router;
    this.i18N = i18N;
    this.dialogService = dialogService;
    this.ea = ea;
    this.user = new User;
    this.localizationService = localizationService;
    this.enterFirstName = this.localizationService.enterFirstName;
    this.enterLastName = this.localizationService.enterLastName;
    this.enterAge = this.localizationService.enterAge;
    this.enterAddress = this.localizationService.enterAddress;
    this.enterEmail = this.localizationService.enterEmail;
    this.enterAsset = this.localizationService.enterAsset;
    this.queryChanged('', '');
  }

  // Associates the Asset. 
  onAssetSelect(ast) {
    ast.assetId = ast.id;

    // Logic to handle the duplication
    let index = this.selectedAssets.findIndex(obj => obj['assetId'] == ast.assetId && obj['name'] == ast.name && obj['symbol'] == ast.symbol);
    if (index >= 0)
      return;

    this.selectedAssets.push(ast);
  }

  // Handle Asset's Autocomplete functionality
  queryChanged(newval, oldval) {
    try {

      this.userService
        .findAsset(newval)
        .then((result) => {
          if (result !== undefined) {
            this.assets = result;
          }
        });

    } catch (error) {
      console.log(error);
    }
  }

  // Disassociate the Asset
  removeAsset(id: any) {
    let index = this.selectedAssets.findIndex(obj => obj['id'] == id);
    this.selectedAssets.splice(index, 1);
  }

  // Displays warning Dialog for reset
  public openDialog() {
    if (!this.isCreatePage) {
      this.router.navigateToRoute("viewProfile", { id: this.user.id });
      return;
    }

    this.dialogService
      .open({
        viewModel: Dialog,
        model: this.localizationService.wngMessageResetData,
      })
      .whenClosed()
      .then((respose) => {
        console.log(respose);
        if (!respose.wasCancelled) {
          this.clearData();
        }
      });
  }

  // Clears the user data
  clearData() {
    this.user = null;
    this.assets = [];
    this.selectedAssets = [];
  }

  // Contains init operation before page load
  activate = async (params) => {
    try {
      if (params.id) {
        this.isCreatePage = false;
        this.user = this.userService.user;
        this.selectedAssets = this.userService.user.assets;
        this.submitBtnCaption = this.localizationService.btnUpdate;
        this.resetBtnCaption = this.localizationService.btnCancel;
        this.title = this.localizationService.titleUpdatePage;
      } else {
        this.submitBtnCaption = this.localizationService.btnCreate;
        this.resetBtnCaption = this.localizationService.btnReset;
        this.title = this.localizationService.titleCreatePage;
      }
      this.setupValidation();
      this.notifier.done();
    } catch (error) {
      console.log(error);
    }
  };

  //this function will fire when user click the submit button
  execute = async () => {
    try {
      var res = await this.controller.validate();

      if (res.valid) {
        this.create();
      }
    } catch (error) {
      console.log(error);
    }
  };

  create = async () => {
    if (this.isCreatePage) {
      this.createUser();
    }
    else {
      this.updateUser();
    }
  };

  // Actions to handle create User
  createUser = async () => {
    try {
      this.user.age = +this.user.age;
      this.user.assets = [];
      this.user.id = 0;
      this.selectedAssets.forEach((element) => {
        this.user.assets.push({ assetId: element.id, name: element.name, symbol: element.symbol });
      })

      await this.userService
        .CreateUser(this.user)
        .then((response) => {
          if (response == undefined) {
            alert(
              this.localizationService.createUserFailed
            );
          } else if (response > 0) {
            this.clearData();
            this.router.navigateToRoute("viewProfile", { id: response, action: "Create" });
          } else {
            alert(this.localizationService.createUserFailed);
          }
        });

    } catch (error) {
      console.log(error);
    }
  };

  // Actions to handle update User
  updateUser = async () => {
    try {
      this.user.age = +this.user.age;
      this.user.assets = [];

      this.selectedAssets.forEach((element) => {
        this.user.assets.push({ assetId: element.assetId, name: element.name, symbol: element.symbol });
      })

      await this.userService
        .updateUser(this.user)
        .then((response) => {
          if (response == undefined) {
            alert(
              this.localizationService.updateUserFailed
            );
          } else if (response > 0) {
            this.clearData();
            this.router.navigateToRoute("viewProfile", { id: response, action: "Update" });
          } else {
            alert(this.localizationService.updateUserFailed);
          }
        });

    } catch (error) {
      console.log(error);
    }
  };

  //enable send button when form validation is done
  get canSave() {
    return (
      this.user.firstName &&
      this.user.lastName &&
      this.user.address &&
      this.user.email &&
      this.user.age >= 18
    );
  }

  //enable reset button when user type something
  get canReset() {
    return (
      this.user.firstName ||
      this.user.lastName ||
      this.user.address ||
      this.user.email ||
      this.user.age
    );
  }

  // Validation setup
  setupValidation() {
    //Custom validation for checking between two numbers
    ValidationRules.customRule(
      "integerRange",
      (value, obj, min, max) => {
        var num = Number.parseInt(value);
        return (
          num === null ||
          num === undefined ||
          (Number.isInteger(num) && num > min && num < max)
        );
      },
      // "${$displayName} must be an integer between ${$config.min} and ${$config.max}.",
      this.localizationService.ageGreaterThan,
      (min, max) => ({ min, max })
    );

    //validation rules starts from here
    ValidationRules.ensure("firstName")
      .displayName(this.localizationService.firstName)
      .required()
      .withMessage(this.localizationService.firstNameReq)
      .minLength(3)
      .withMessage(this.localizationService.firstNameMinLen)
      .maxLength(250)
      .withMessage(this.localizationService.firstNameMaxLen)

      .ensure("lastName")
      .displayName(this.localizationService.lastName)
      .required()
      .withMessage(this.localizationService.lastNameReq)
      .minLength(3)
      .withMessage(this.localizationService.lastNameMinLen)
      .maxLength(250)
      .withMessage(this.localizationService.lastNameMaxLen)

      .ensure("age")
      .displayName(this.localizationService.age)
      .required()
      .withMessage(this.localizationService.ageReq)
      .satisfiesRule("integerRange", 18, 150)

      .ensure("address")
      .displayName(this.localizationService.address)
      .required()
      .withMessage(this.localizationService.addressReq)
      .minLength(10)
      .withMessage(this.localizationService.addressMinLen)
      .maxLength(250)
      .withMessage(this.localizationService.addressMaxLen)

      .ensure("email")
      .displayName(this.localizationService.email)
      .required()
      .withMessage(this.localizationService.emailReq)
      .email()
      .withMessage(this.localizationService.emailInValid)
      .maxLength(250)
      .withMessage(this.localizationService.emailMaxLen)

      .on(this.user);
  }


}
