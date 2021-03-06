import 'react-app-polyfill/ie9';
import 'core-js/es/object';
import 'core-js/es/array/includes';

import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import Keycloak from 'keycloak-js';

import App from './js/App';
import store from './js/store';
import Api from './js/api/api';
import initFontAwesome from './js/fontAwesome';

import { UPDATE_AUTH_USER } from './js/actions/types';

initFontAwesome();

const keycloakConfig = {
  url: window.RUNTIME_REACT_APP_SSO_HOST ? window.RUNTIME_REACT_APP_SSO_HOST : process.env.REACT_APP_SSO_HOST,
  realm: window.RUNTIME_REACT_APP_SSO_REALM ? window.RUNTIME_REACT_APP_SSO_REALM : process.env.REACT_APP_SSO_REALM,
  clientId: window.RUNTIME_REACT_APP_SSO_CLIENT
    ? window.RUNTIME_REACT_APP_SSO_CLIENT
    : process.env.REACT_APP_SSO_CLIENT,
};

const keycloak = Keycloak(keycloakConfig);

keycloak.onAuthSuccess = () => {
  getKeycloakUserInfo();
};

keycloak.onAuthRefreshSuccess = () => {
  getKeycloakUserInfo();
};

function getKeycloakUserInfo() {
  keycloak.loadUserInfo().success(data => {
    store.dispatch({ type: UPDATE_AUTH_USER, payload: data });
  });
}

keycloak
  .init({ onLoad: 'login-required' })
  .success(authenticated => {
    if (authenticated) {
      ReactDOM.render(
        <Provider store={store}>
          <App />
        </Provider>,
        document.getElementById('root')
      );
    }
  })
  .error(() => {
    //alert('failed to initialize');
  });

Api.interceptors.request.use(
  config =>
    new Promise(resolve =>
      keycloak
        .updateToken(5)
        .success(() => {
          config.headers.Authorization = `Bearer ${keycloak.token}`;
          resolve(config);
        })
        .error(() => {
          keycloak.login();
        })
    )
);
