import { Result } from "../../shared/model/result.model";

export interface LoginResponse {
  result: Result;
  token: string;
  role?: string;
}

