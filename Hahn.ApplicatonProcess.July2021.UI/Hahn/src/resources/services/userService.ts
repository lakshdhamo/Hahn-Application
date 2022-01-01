import { User } from './../models/user';
import { HttpClient, json } from "aurelia-fetch-client";
import { inject } from "aurelia-framework";
import { appValue } from "../config/appValue";

@inject(HttpClient, User)
export class UserService {
  private http: HttpClient;
  public user: User;
  public assets: any;

  constructor(http: HttpClient, user: User) {
    this.user = user;
    http.configure((config) => {
      config
        .withBaseUrl(appValue.apiUrl)
        .withDefaults({
          credentials: "same-origin",
          headers: {
            Accept: "application/json",
            "X-Requested-With": "Fetch",
          },
        })
        .withInterceptor({
          request(request) {
            console.log(`Requesting ${request.method} ${request.url}`);
            return request; // you can return a modified Request, or you can short-circuit the request by returning a Response
          },
          response(response) {
            console.log(`Received ${response.status} ${response.url}`);
            return response; // you can return a modified Response
          },
        });
    });
    this.http = http;
  }

  CreateUser(user) {
    return this.http
      .fetch("users", {
        method: "post",
        body: json(user),
      })
      .then((response) => response.json())
      .then((responseMessage) => {
        return responseMessage;
      })
      .catch((error) => {
        console.log("Error saving user", error);
      });
  }

  getUserById(id) {
    return this.http
      .fetch(`users/${id}`)
      .then((response) => response.json())
      .then((user) => {
        return user;
      })
      .catch((error) => {
        console.log("Error retrieving user.", error);
        return [];
      });
  }

  updateUser(user) {
    return this.http
      .fetch(`users/${user.id}`, {
        method: "put",
        body: json(user),
      })
      .then((response) => response.json())
      .then((responseMessage) => {
        return responseMessage;
      })
      .catch((error) => {
        console.log("Error saving user", error);
      });
  }

  deleteUserProfile(id) {
    return this.http
      .fetch(`users/${id}`, {
        method: "delete",
      })
      .then((response) => response.json())
      .then((responseMessage) => {
        return responseMessage;
      })
      .catch((error) => {
        console.log("Error deleting user", error);
      });
  }

  findAsset() {
    return this.http
      .fetch('assets')
      .then((response) => response.json())
      .then((asset) => {
        this.assets = asset;
        return asset;
      })
      .catch((error) => {
        console.log("Error retrieving user.", error);
        return [];
      });
  }

  getAssetDetails(assetId) {
    return this.http
      .fetch(`assets/${assetId}`)
      .then((response) => response.json())
      .then((assetDetail) => {
        return assetDetail;
      })
      .catch((error) => {
        console.log("Error retrieving asset details.", error);
        return [];
      });

  }

}
