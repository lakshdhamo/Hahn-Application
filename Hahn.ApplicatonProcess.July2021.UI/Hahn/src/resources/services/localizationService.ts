import { inject } from "aurelia-framework";
import { I18N } from "aurelia-i18n";

@inject(
  I18N
)
export class LocalizationService {
  private i18n: I18N;
  public age: string;
  public firstNameMinLen: string;
  public firstNameReq: string;
  public firstName: string;
  public lastNameMinLen: string;
  public lastNameReq: string;
  public lastName: string;
  public ageReq: string;
  public Age: string;
  public addressMinLen: string;
  public addressReq: string;
  public address: string;
  public emailInValid: string;
  public emailReq: string;
  public email: string;

  public addSuccessMsg: string;
  public updateSuccessMsg: string;
  public btnCreate: string;
  public btnReset: string;
  public btnUpdate: string;
  public btnCancel: string;
  public titleCreatePage: string;
  public titleUpdatePage: string;
  public appTitle: string;
  public titleViewPage: string;
  public wngMessageResetData: string;
  public createUserFailed: string;
  public updateUserFailed: string;
  public enterFirstName: string;
  public enterLastName: string;
  public enterAge: string;
  public enterAddress: string;
  public enterEmail: string;
  public associateAssetTitle: string;
  public id: string;
  public name: string;
  public symbol: string;
  public delete: string;
  public ageGreaterThan: string;
  public deleteUserWarningMsg: string;
  public btnEditProfile: string;
  public btnDeleteProfile: string;
  public userProfile: string;
  public assetLiveData: string;
  public enterAsset: string;
  public assetDetailFetchError: string;
  public error: string;
  public viewErrorMsg: string;

  constructor(i18n) {
    this.i18n = i18n;

    this.firstNameMinLen = this.i18n.tr('firstNameMinLen');
    this.firstNameReq = this.i18n.tr('firstNameReq');
    this.firstName = this.i18n.tr('firstName');
    this.enterFirstName = this.i18n.tr('enterFirstName');
    this.lastNameMinLen = this.i18n.tr('lastNameMinLen');
    this.lastNameReq = this.i18n.tr('lastNameReq');
    this.lastName = this.i18n.tr('lastName');
    this.enterLastName = this.i18n.tr('enterLastName');
    this.ageReq = this.i18n.tr('ageReq');
    this.age = this.i18n.tr('age');
    this.enterAge = this.i18n.tr('enterAge');
    this.addressMinLen = this.i18n.tr('addressMinLen');
    this.addressReq = this.i18n.tr('addressReq');
    this.address = this.i18n.tr('address');
    this.enterAddress = this.i18n.tr('enterAddress');
    this.emailInValid = this.i18n.tr('emailInValid');
    this.emailReq = this.i18n.tr('emailReq');
    this.email = this.i18n.tr('email');
    this.enterEmail = this.i18n.tr('enterEmail');

    this.addSuccessMsg = this.i18n.tr('addSuccessMsg');
    this.updateSuccessMsg = this.i18n.tr('updateSuccessMsg');
    this.btnCreate = this.i18n.tr('btnCreate');
    this.btnReset = this.i18n.tr('btnReset');
    this.btnUpdate = this.i18n.tr('btnUpdate');
    this.btnCancel = this.i18n.tr('btnCancel');
    this.titleCreatePage = this.i18n.tr('createPageTitle');
    this.titleUpdatePage = this.i18n.tr('titleUpdatePage');
    this.titleViewPage = this.i18n.tr('titleViewPage');
    this.appTitle = this.i18n.tr('appTitle');
    this.wngMessageResetData = this.i18n.tr('wngMessageResetData');
    this.createUserFailed = this.i18n.tr('createUserFailed');
    this.updateUserFailed = this.i18n.tr('updateUserFailed');
    this.associateAssetTitle = this.i18n.tr('associateAssetTitle');

    this.id = this.i18n.tr('id');
    this.name = this.i18n.tr('name');
    this.symbol = this.i18n.tr('symbol');
    this.delete = this.i18n.tr('delete');
    this.ageGreaterThan = this.i18n.tr('ageGreaterThan');
    this.deleteUserWarningMsg = this.i18n.tr('deleteUserWarningMsg');
    this.btnEditProfile = this.i18n.tr('btnEditProfile');
    this.btnDeleteProfile = this.i18n.tr('btnDeleteProfile');
    this.userProfile = this.i18n.tr('userProfile');
    this.assetLiveData = this.i18n.tr('assetLiveData');
    this.enterAsset = this.i18n.tr('enterAsset');
    this.assetDetailFetchError = this.i18n.tr('assetDetailFetchError');
    this.error = this.i18n.tr('error');
    this.viewErrorMsg = this.i18n.tr('viewErrorMsg');
    
    

  }

}
