module Dto {$Classes(*Model)[
    export interface $Name$TypeParameters {$Properties[
        $name: $Type;]
    }]
}

${
    Template(Settings settings)
    {
        settings.OutputFilenameFactory = file => 
        {
			var fileName = file.Name.Replace(".cs", "");
			fileName = fileName.Replace("Model", "");
			fileName = fileName.ToLower() + ".model.ts";
            return fileName;
        };
    }
}