import { LocalizationService } from './../../resources/services/localizationService';
import { Asset } from './../../resources/models/asset';
import { UserService } from './../../resources/services/userService';
import {
  inject, CompositionTransaction,
  CompositionTransactionNotifier,
} from "aurelia-framework";
import { User } from '../../resources/models/user';
import { DialogService } from 'aurelia-dialog';
import { Dialog } from "../../resources/dialog/dialog";
import { Router } from "aurelia-router";

@inject(
  UserService,
  CompositionTransaction,
  // User,
  // Asset,
  DialogService,
  Router,
  LocalizationService
)
export class ViewUserProfile {
  private userService: UserService;
  private notifier: CompositionTransactionNotifier;
  user: User;
  selectedAsset: Asset;
  private dlg: DialogService;
  private router: Router;
  private localizationService: LocalizationService;
  private btnEditProfile: string;
  private btnDeleteProfile: string;
  private userProfile: string;
  private assetLiveData: string;
  private selectedRowName = "";
  private error: string;
  private showError = false;
  private errorMsg: string;

  constructor(
    userService: UserService,
    compositionTransaction: { enlist: () => CompositionTransactionNotifier; },
    dlg: DialogService,
    router: Router,
    localizationService: LocalizationService) {
    this.userService = userService;
    this.notifier = compositionTransaction.enlist();
    this.dlg = dlg;
    this.router = router;
    this.localizationService = localizationService;
    this.btnEditProfile = this.localizationService.btnEditProfile;
    this.btnDeleteProfile = this.localizationService.btnDeleteProfile;
    this.userProfile = this.localizationService.userProfile;
    this.assetLiveData = this.localizationService.assetLiveData;
    this.error = this.localizationService.error;
  }

  // Contains init operation before page load
  activate = async (params) => {
    try {
      await this.userService.getUserById(params.id)
        .then((response) => {
          this.user = response
          if (response.length == 0) {
            this.errorMsg = this.localizationService.viewErrorMsg;
            this.showError = true;
            setTimeout(() => {
              this.showError = false;
            }, 2000);
          }
        });
      this.notifier.done();

    } catch (error) {
      console.log(error);
    }
  };

  // Fetch the detailed Asset information
  setSelected(ast) {
    this.selectedRowName = ast.name;

    this.userService.getAssetDetails(ast.assetId)
      .then((response) => {
        this.selectedAsset = response;
        if (response.length == 0) {
          this.errorMsg = this.localizationService.assetDetailFetchError;
          this.showError = true;
          setTimeout(() => {
            this.showError = false;
          }, 2000);
        }
      }
      );

  }

  // Edit profile operation
  EditProfile() {
    this.router.navigateToRoute("editProfile", { id: this.user.id });
    this.userService.user = this.user;
  }

  // Delete profile operation
  DeleteProfile() {
    this.dlg
      .open({
        viewModel: Dialog,
        model: this.localizationService.deleteUserWarningMsg,
      })
      .whenClosed()
      .then(async (result) => {
        if (!result.wasCancelled) {
          await this.userService.deleteUserProfile(this.user.id);
          this.userService.user = null;
          this.router.navigateToRoute("createProfile");
        } else {
          console.log("cancelled");
        }
      })
  }

}
