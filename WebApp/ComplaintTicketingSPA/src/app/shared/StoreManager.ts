export class StoreManager {
    public static sessionStorageSetItem(key: string, data: any) {
        sessionStorage.setItem(key, JSON.stringify(data));
    }

    public static sessionStorageGetItem(key: string) {
        let item = sessionStorage.getItem(key)
        if(!!item)
            return StoreManager.JSonTryParse(item);
        
        return '';
    }

    public static clearSessionStorageByKey(key: string) {
        sessionStorage.removeItem(key);
    }

    private static JSonTryParse(value: string) {
        try {
            return JSON.parse(value);
        }
        catch (e) {
            if (value === "undefined")
                return void 0;
            return value;
        }
    }
}