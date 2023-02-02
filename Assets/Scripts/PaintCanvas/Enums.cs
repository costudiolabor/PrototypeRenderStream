namespace Enums {
    public enum BrushType {
        Line = 0,
        Arrow = 1,
    }

    public enum HighlightState {
        NotSet = -1,
        InverterQ1Q2Q4 = 0,
        Inverter = 1,
        InverterQ1 = 2,
        InverterQ1Rectifier = 3,
        InverterQ1Q2 = 4,
        InverterQ1Q4 = 5,
        InverterQ2 = 6,
        InverterQ2Q4 = 7,
        Q1Q2Q4 = 8,
        Q1 = 9,
        Q1Rectifier = 10,
        Q1Q4 = 11,
        Q1Q2 = 12,
        Q2 = 13,
        Q2Q4 = 14,
        Q4 = 15,
        AllOff = 16,
    }

    public enum HttpStatus : int {
        ReadyToReceive = -1,
        AppError = 0,
        IsOk = 200,
        
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500,
        BadGateway = 502,
        ServiceUnavailable = 503,
    }

    public enum RequestMethod {
        POST = 0,
        GET = 1,
        PUT = 2
    }

    public enum ARPlacementStates {
        None,
        Initialize,
        FreeScan,
        ScanSelected
    }

    public enum WorkInARMode {
        None,
        Selected,
        FreeScan
        
    }

    public enum WorkLogStatuses {
        COMPLETED,
        IN_WORK,
         
    }
    
}
