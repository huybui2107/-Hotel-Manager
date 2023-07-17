import http from "../utils/http";

const authApi = {
  register(data) {
    return http.post("user/register", data);
  },
  login(data) {
    return http.post("auth/login", data);
  },
  registerMember(data) {
    return http.post("user/registerManage", data);
  },
  updateProfile(data) {
    return http.put("user/updateProfile", data);
  },
  changePass(data) {
    return http.put("user/changePassword", data);
  },
  logout() {
    return;
  },
};

export default authApi;
